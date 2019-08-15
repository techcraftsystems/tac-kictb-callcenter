using System;

namespace Core.ViewModel
{
    public class Breadcrumbs
    {
        public int Style { get; set; }
        public string Title { get; set; }
        public Linkage Level1 { get; set; }
        public Linkage Level2 { get; set; }
        public Linkage Level3 { get; set; }
        public Linkage Level4 { get; set; }

        public Breadcrumbs() {
            Style = 0;
            Title = "";

            Level1 = new Linkage();
            Level2 = new Linkage();
            Level3 = new Linkage();
            Level4 = new Linkage();
        }
    }

    public class Linkage {
        public string Url { get; set; }
        public string Label { get; set; }

        public Linkage() {
            Url = "";
            Label = "";
        }
    }
}
