using System;
using System.Collections.Generic;
using System.Text;

namespace MusicViewer
{
    interface IMusicRepository
    {
        IEnumerable<Album> ListAlbums();
    }
}
