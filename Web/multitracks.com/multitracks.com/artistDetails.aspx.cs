using DataAccess;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class artistDetails : System.Web.UI.Page
{
    private int _artistID;
    public ArtistModel artistModel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
            return;

        errorMessage.Visible = false;
        errorMessage.InnerText = string.Empty;

        try
        {
            if (!string.IsNullOrEmpty(Request.QueryString["artistID"]))
            {
                string searchTerm = Request.QueryString["artistID"];
                _artistID = Convert.ToInt32(searchTerm);
            }
            else
            {
                throw new KeyNotFoundException();
            }

            var sql = new SQL();

            form.Visible = true;

            sql.Parameters.Add("@artistID", _artistID);
            DataSet dataSet = sql.ExecuteStoredProcedureDS("GetArtistDetails");

            DataRow artistRow = dataSet.Tables[0].Rows[0];

            artistModel = JsonConvert.DeserializeObject<ArtistModel>(
                JsonConvert.SerializeObject(artistRow.ToDynamic()));

            DataTable albuns = dataSet.Tables[1];
            albumItems.DataSource = albuns;
            albumItems.DataBind();
            
            DataTable allAlbuns = albuns.Clone();
            allAlbunsItems.DataSource = albuns;
            allAlbunsItems.DataBind();

            DataTable songs = dataSet.Tables[2];
            playlistItems.DataSource = songs;
            playlistItems.DataBind();

            // fake top 3 songs
            DataTable topSongs = songs.Clone();
            for (int i = 0; i < 3; i++)
            {
                topSongs.ImportRow(songs.Rows[i]);
            }

            topSongsItems.DataSource = topSongs;
            topSongsItems.DataBind();

        }
        catch (KeyNotFoundException)
        {
            form.Visible = false;
            errorMessage.Visible = true;
            errorMessage.InnerText = "Artist not found. Send a valid parameter in the URL.";
        }
        catch (Exception ex)
        {
            form.Visible = false;
            errorMessage.Visible = true;
            errorMessage.InnerText = ex.Message;
        }
    }
}

