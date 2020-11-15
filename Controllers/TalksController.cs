using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/camps/{moniker}/talks")]
    public class TalksController : ApiController
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;

        public TalksController(ICampRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        // GET: api/Camps/ATL2018/talks
        // GET: api/Camps/ATL2018/talks?includeSpeakers=true            // Query Strings should always be optional.
        [Route()]                                                       // the URI pattern is here empty, because we use the [RoutePrefix] attribute for the whole Controller.
        [HttpGet]                                                       // unnecessary here...
        [ResponseType(typeof(TalkDto[]))]                               // helpful for creating RESTful Web APIs and also when autogenerating documentation via Swagger.
        public async Task<IHttpActionResult> Get(string moniker, bool includeSpeakers = false)
        {
            try
            {
                Talk[] talks = await _repository.GetTalksByMonikerAsync(moniker, includeSpeakers);

                if (talks == null || talks.Length == 0)               
                {
                    //return NotFound();                                // in this case return OK with an empty array...
                }

                //TalkDto[] mappedTalks = _mapper.Map<Talk[], TalkDto[]>(talks);
                var mappedTalks = _mapper.Map<IEnumerable<TalkDto>>(talks);
                return Ok(mappedTalks);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        // GET: api/Camps/ATL2018/talks/5
        // GET: api/Camps/ATL2018/talks/5?includeSpeakers=true           // Query Strings should always be optional.
        [Route("{id:int}", Name="GetTalk")]
        [HttpGet]                                                       
        [ResponseType(typeof(TalkDto))]                               
        public async Task<IHttpActionResult> GetTalk(string moniker, int id, bool includeSpeakers = false)
        {
            try
            {
                Talk talk = await _repository.GetTalkByMonikerAsync(moniker, id, includeSpeakers);

                if (talk == null)
                {
                    return NotFound();
                }

                //TalkDto mappedTalk = _mapper.Map<Talk, TalkDto>(talk);
                TalkDto mappedTalk = _mapper.Map<TalkDto>(talk);
                return Ok(mappedTalk);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        
        // POST: api/Camps/ATL2018/talks
        [Route()]                                                 
        [HttpPost]
        [ResponseType(typeof(TalkDto))]
        public async Task<IHttpActionResult> Post(string moniker, TalkDto talkDto)
        {
            try
            {
                if (await _repository.GetTalkByMonikerAsync(moniker, talkDto.TalkId) != null)
                {
                    ModelState.AddModelError("TalkId", "TalkId is already in use inside this camp..!");                     // optional because EF generates a new id every time. The id inside the body won't be used...
                }

                if (ModelState.IsValid)
                {
                    Talk talk = _mapper.Map<Talk>(talkDto);

                    // Map the camp to the talk (camp is here a foreign key for the Talk class/model and it's not a part of the TalkDto)...
                    Camp camp = await _repository.GetCampAsync(moniker);
                    if (camp == null)
                    {
                        return BadRequest("No camp found for this moniker..!");
                    }
                    talk.Camp = camp;

                    // If a speaker id is given, map the speaker to the talk...
                    if (talkDto.Speaker != null) 
                    {
                        Speaker speaker = await _repository.GetSpeakerAsync(talkDto.Speaker.SpeakerId);
                        if (speaker == null)
                        {
                            return BadRequest("No speaker found with this Id..!");
                        }
                        talk.Speaker = speaker;                                                                             // append the speaker data for the given id to the talk to be created...
                    }

                    _repository.AddTalk(talk);

                    if (await _repository.SaveChangesAsync())
                    {
                        TalkDto newTalk = _mapper.Map<TalkDto>(talk);

                        return Created(new Uri(Request.RequestUri + "/" + newTalk.TalkId), newTalk);
                        //return CreatedAtRoute("GetTalk", new { moniker = moniker, talkId = talk.TalkId }, newTalk);       // TODO: Exception "UrlHelper.Link must not return null".   But why???
                    }
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return BadRequest(ModelState);
        }
    }
}
