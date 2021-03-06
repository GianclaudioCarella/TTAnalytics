﻿using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using System.Net;
using TTAnalytics.Data;
using TTAnalytics.Model;
using TTAnalytics.Repository;
using TTAnalytics.RepositoryInterface;

namespace TTAnalytics.API.Controllers
{
    /// <summary>
    /// ClubController
    /// </summary>
    [RoutePrefix("api/club")]
    public class ClubController : ApiController
    {
        private IClubRepository clubRepository;
        private IPlayerRepository playerRepository;

        /// <summary>
        /// ClubController
        /// </summary>
        public ClubController()
        {
            clubRepository = new ClubRepository(new TTAnalyticsContext());
            playerRepository = new PlayerRepository(new TTAnalyticsContext());
        }

        /// <summary>
        /// ClubController
        /// </summary>
        /// <param name="clubRepository"></param>
        /// <param name="playerRepository"></param>
        public ClubController(IClubRepository clubRepository, IPlayerRepository playerRepository)
        {
            this.clubRepository = clubRepository;
            this.playerRepository = playerRepository;
        }

        /// <summary>
        /// Get List of All Clubs
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Get List of All Clubs")]
        [ResponseType(typeof(ICollection<Club>))]
        public IHttpActionResult Get()
        {
            return Json(clubRepository.GetAll());
        }

        /// <summary>
        /// Get Specific Club by Id
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Get Specific Club by Id")]
        [ResponseType(typeof(Club))]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return Json(clubRepository.Get(id));
        }

        /// <summary>
        /// Get List of All Players from a Club
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Get List of All Players from a Club")]
        [ResponseType(typeof(ICollection<Club>))]
        [Route("{id:int}/players")]
        public IHttpActionResult GetClubPlayers(int id)
        {
            return Json(playerRepository.GetByClub(id));
        }

        /// <summary>
        /// Add New Club
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Add New Club")]
        [ResponseType(typeof(Club))]
        public IHttpActionResult Post([FromBody]Club club)
        {
            var result = new Club();

            if (ModelState.IsValid)
            {
                if (club.Country == null)
                {
                    return Content(HttpStatusCode.BadRequest, "Club is empty");
                }

                if (club.Country.Id == 0 || clubRepository.Get(club.Country.Id) == null)
                {
                    return Content(HttpStatusCode.NotFound, "Club not found");
                }
                
                result = clubRepository.Add(club);
            }

            return Json(result);
        }

        /// <summary>
        /// Edit Club
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Edit Club")]
        [ResponseType(typeof(Club))]
        public IHttpActionResult Put([FromBody]Club club)
        {
            var result = new Club();

            if (ModelState.IsValid)
            {
                club = clubRepository.Update(club);
            }

            return Json(club);
        }

        /// <summary>
        /// Delete Club
        /// </summary>
        /// <returns></returns>
        [SwaggerOperation("Delete Club")]
        [ResponseType(typeof(void))]
        public void Delete(int id)
        {
            clubRepository.Delete(id);
        }
    }
}