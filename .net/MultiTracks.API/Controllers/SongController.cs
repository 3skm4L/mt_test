using DataAccess;
using MultiTracks.API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;

namespace MultiTracks.API.Controllers
{
    [RoutePrefix("song")]
    public class SongController : ApiController
    {
        [HttpGet]
        [Route("list")]
        public List<SongModel> ListSongs(int artistId = -1, int pageNumber = 0, int pageSize = 10)
        {
            var sql = new SQL();
            DataTable table;
            int offset = pageNumber * pageSize;
            sql.Parameters.Add("@offset", offset);
            sql.Parameters.Add("@pageSize", pageSize);
            if (artistId == -1)
            {
                 table = sql.ExecuteDT("SELECT * FROM [Song] order by title " +
                     "offset @offset rows fetch next @pageSize rows only");
            }
            else
            {
                sql.Parameters.Add("@artistId", artistId);
                table = sql.ExecuteDT("SELECT * FROM [Song] where artistId = @artistId order by title " +
                     "offset @offset rows fetch next @pageSize rows only");
            }
            List<SongModel> songList = new List<SongModel>();
            foreach (DataRow song in table.Rows)
            {
                var songModel = new SongModel();
                songModel.SongID = (int)song["SongID"];
                songModel.dateCreation = song["dateCreation"].ToString();
                songModel.albumID = (int)song["albumID"];
                songModel.timeSignature = song["timeSignature"].ToString();
                songModel.artistID = (int)song["artistID"];
                songModel.title = song["title"].ToString();
                songModel.patches = (bool)song["patches"];
                songModel.bpm = (decimal)song["bpm"];
                songModel.chart = (bool)song["chart"];
                songModel.customMix = (bool)song["customMix"];
                songModel.rehearsalMix = (bool)song["rehearsalMix"];
                songModel.multitracks = (bool)song["multitracks"];
                songModel.songSpecificPatches = (bool)song["songSpecificPatches"];
                songModel.proPresenter = (bool)song["proPresenter"];
                songList.Add(songModel);
            }
            return songList;
        } 

    }
}
