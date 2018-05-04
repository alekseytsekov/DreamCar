using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamCar.Models.Common
{
    public class Paginator
    {
        private const int DefaultEntitiesPerPage = 5;

        public Paginator()
        {
            this.CurrentPage = 1;
            this.EntitiesPerPage = DefaultEntitiesPerPage;
        }

        public Paginator(int entitiesPerPage = DefaultEntitiesPerPage)
        {
            this.CurrentPage = 1;
            this.EntitiesPerPage = entitiesPerPage;
        }

        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        
        public int EntitiesPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; private set; }

        public int TotalMatches { get; private set; }

        public void SetTotals(int collectionCount)
        {
            if (collectionCount < 1)
            {
                this.TotalPages = 1;
                this.TotalMatches = 0;
                return;
            }

            this.TotalPages = collectionCount / this.EntitiesPerPage + 1;

            if (collectionCount % this.EntitiesPerPage == 0)
            {
                this.TotalPages--;
            }

            this.TotalMatches = collectionCount;
        }

        public int Take
        {
            get
            {
                return this.EntitiesPerPage;
            }
        }

        public int Skip()
        {
            if (this.CurrentPage > this.TotalPages)
            {
                this.CurrentPage = this.TotalPages;
            }

            if (this.CurrentPage - 1 < 0)
            {
                this.CurrentPage = 1;
            }

            return (this.CurrentPage - 1) * this.EntitiesPerPage;
        }
        
    }
}
