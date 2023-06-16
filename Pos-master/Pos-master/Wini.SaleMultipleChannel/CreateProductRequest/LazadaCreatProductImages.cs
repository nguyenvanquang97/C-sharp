using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wini.SaleMultipleChannel.CreateProductRequest

{
    public class LazadaCreatProductImages
    {
        public LazadaCreatProductImages()
        {
        }

        public LazadaCreatProductImages(List<string> image)
        {
            Image = image;
        }

        public List<string> Image { get; set; }
    }
}
