﻿using BLL.Interface.Services;
using MvcPL.ViewModels;
using MvcPL.Infrastructure.Mappers;
using System.Web.Mvc;
using System.Linq;
using System;

namespace MvcPL.Controllers
{
    public class VotingController : Controller
    {
        #region Fields
        private readonly IVotingService votingService;
        private readonly IUserService userService;
        private readonly IPhotoService photoService;
        #endregion

        #region Ctors
        public VotingController(IPhotoService phService, IUserService usrService, IVotingService voteService)
        {
            photoService = phService;
            userService = usrService;
            votingService = voteService;
        }
        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddVoting(string userName, int photoId, int rating)
        {
            int userId = userService.GetUserByEmail(userName).UserId;
            votingService.Create((new VotingViewModel() { UserId = userId, PhotoId = photoId, Rating = rating }).ToBllVoting());

            int raters = GetRaters(photoId);
            double totalRating = Math.Round(GetTotalRating(photoId), 1);
            var jsondata = new { Raters = raters, Rating = totalRating };// ctx.Entries.Where(e => e.Message.Contains(searchString));

            return Json(jsondata);
        }

        public double GetTotalRating(int photoId)
        {
            double total = votingService.GetRatingForPhoto(photoId);

            return Math.Round(total, 1);
        }

        public int GetRaters(int photoId)
        {
            int raters = 0;
            var votes = votingService.GetAll().Select(vote => new VotingViewModel
            {
                UserId = vote.UserId,
                PhotoId = vote.PhotoId,
                Rating = vote.Rating
            });

            if (votes != null)             
                foreach (var vote in votes)
                    if (vote.PhotoId == photoId)
                        raters++;

            return raters;
        }

        public bool CanVote(string userName, int photoId)
        {
            var user = userService.GetUserByEmail(userName);

            if(user != null)
                if (votingService.GetRatingForPhotoUser(photoId, user.UserId) > 0)
                    return false;

            return true;
        }

        public bool IsUserPhoto(string userName, int photoId)
        {
            var user = userService.GetUserByEmail(userName);

            if (user != null)
                if (photoService.GetPhotoEntity(photoId).UserId == user.UserId)
                    return true;

            return false;
        }

        #endregion
    }
}