using DataAccess;
using System;
using System.Activities.Statements;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ArtistDetails : MultitracksPage
{
    protected string biography { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        var sql = new SQL();
        int artistId = 2;
        string param = Request.QueryString["artistId"];
        if (!string.IsNullOrEmpty(param))
        {
            try { artistId = int.Parse(param); }
            catch { }
        }
        
        try
        {

            sql.Parameters.Add("@artistId", artistId);
            DataSet artistDetails = sql.ExecuteStoredProcedureDS("GetArtistDetails");


            //var albumData = sql.ExecuteDT("SELECT * FROM [multitracks].[dbo].[Album] where artistID = @artistId");
            //sql.Parameters.Clear();
            //sql.Parameters.Add("@artistId", artistId);
            //var songData = sql.ExecuteDT("SELECT song.title as songTitle, album.title as albumTitle, bpm, imageURL " +
            //    "FROM [multitracks].[dbo].[Song] song join [dbo].Album album on song.albumID = album.albumID  where album.artistID = @artistId");
            //sql.Parameters.Clear();
            //sql.Parameters.Add("@artistId", artistId);
            //var artistData = sql.ExecuteDT("Select title, biography, imageURL, heroURL " +
            //    "from [multitracks].[dbo].[Artist] where artistID = @artistId");
            
            songItems.DataSource = artistDetails.Tables[1];
            songItems.DataBind();

           albumItems.DataSource = artistDetails.Tables[0];
           albumItems.DataBind();

            DataRow row = artistDetails.Tables[2].Rows[0];

            imageHero.ImageUrl = row["heroURL"].ToString();
            imageSmall.ImageUrl = row["imageURL"].ToString();
            bandName.Text = row["title"].ToString();
            biography = row["biography"].ToString();
            
            int index = biography.IndexOf("<!-- read more -->");
            if (index != -1)
                {
                    biography = biography.Substring(0, index);
                    restBio.Text = row["biography"].ToString().Substring(index);
                }
            biography = biography.Replace("\n", "</p> <p>");
            //publishDB.Visible = false;
            //items.Visible = true;
        }
        catch
        {
            //publishDB.Visible = true;
            //items.Visible = false;
        }
    }

    protected void readMore(object sender, EventArgs e)
    {
        if (!restBio.Visible)
        {
            restBio.Visible = true;
            lbReadMore.Text = "Read Less...";
        }
        else
        {
            restBio.Visible = false;
            lbReadMore.Text = "Read More...";
        }
    }
}