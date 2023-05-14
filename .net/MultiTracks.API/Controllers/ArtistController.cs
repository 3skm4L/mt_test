using DataAccess;
using Microsoft.AspNet.Identity;
using MultiTracks.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MultiTracks.API.Controllers
{
    [RoutePrefix("artist")]
    public class ArtistController : ApiController
    {
        [HttpGet]
        [Route("search")]
        public IEnumerable<ArtistModel> SearchArtist(String name)
        {
            var sql = new SQL();
            sql.Parameters.Add("@name", "%" + name + "%");
            var artistData = sql.ExecuteDT("SELECT * FROM [multitracks].[dbo].[Artist] where title like @name");
            List<ArtistModel> artistList = new List<ArtistModel>();
            foreach (DataRow artist in artistData.Rows)
            {
                var artistModel = new ArtistModel();
                artistModel.title = artist["title"].ToString();
                artistModel.imageURL = artist["imageURL"].ToString();
                artistModel.heroURL = artist["heroURL"].ToString();
                artistModel.biography = artist["biography"].ToString();
                artistModel.dateCreation = artist["dateCreation"].ToString();
                artistModel.artistID = (int)artist["artistID"];
                artistList.Add(artistModel);
            }
            return artistList;
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddArtist([FromBody] ArtistModel model) 
        {
            if (!this.ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            } 
            try
            {
                var sql = new SQL();
                sql.Parameters.Add("@title", model.title);
                sql.Parameters.Add("@biography", model.biography);
                sql.Parameters.Add("@imageURL", model.imageURL);
                sql.Parameters.Add("@heroURL", model.heroURL);
                sql.Execute("insert into[multitracks].[dbo].[Artist](title, biography, imageURL, heroURL) " +
                    "values(@title, @biography, @imageURL, @heroURL)");
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
