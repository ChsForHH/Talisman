using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talisman.Domain.Entities;

namespace Talisman.Domain.Abstract
{
    public interface IGlobalData
    {
        List<Tovar> Tovars { get; }
        List<Categorie> Categories { get; }
        List<Article> Articles { get; }
        List<ArtMin> ArtMins { get; }
        List<Photo> Photos { get; }
        //IEnumerable<Photo> Images { get; }
        List<Service> Services { get; }
        List<FeedBack> FeedBacks { get; }
        List<New> News { get; }
        List<TovarsAndPhoto> TaP { get; }
        Over Over { get; }       
    }
    
}
