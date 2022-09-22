using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaShpInstaller
{
    public sealed class ViewModel : INotifyPropertyChanged
    {
        public sealed class SpeciesEntry
        {
            public int Id { get; set; }

            public short Species { get; set; }

            public XwaSpecDescEntry SpecDesc { get; set; }

            public XwaShipListEntry ShipList { get; set; }

            public string ModelName { get; set; }
        }

        public sealed class ObjectEntry
        {
            public int Id { get; set; }

            public XwaExeObjectEntry Object { get; set; }

            public string ModelName { get; set; }
        }

        public sealed class CraftEntry
        {
            public int Id { get; set; }

            public XwaExeCraftEntry Craft { get; set; }

            public XwaExeObjectEntry Object { get; set; }

            public string ModelName { get; set; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private XwaShpFile _shpFile;

        public ViewModel()
        {
        }

        public ViewModel(string path)
        {
            var workspace = new XwaWorkspace(path);

            this.WorkingDirectory = path;
            this.Workspace = workspace;
            this.SetSpecies(workspace);
            this.SetObjects(workspace);
            this.SetCrafts(workspace);
        }

        public string WorkingDirectory { get; set; }

        public XwaWorkspace Workspace { get; set; }

        public List<SpeciesEntry> Species { get; set; }

        public List<ObjectEntry> Objects { get; set; }

        public List<CraftEntry> Crafts { get; set; }

        public XwaShpFile ShpFile
        {
            get
            {
                return this._shpFile;
            }

            set
            {
                this._shpFile = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShpFile)));
            }
        }

        private void SetSpecies(XwaWorkspace workspace)
        {
            int num = 0;
            var r = from entry in workspace.SpeciesTable.Entries
                    select new SpeciesEntry
                    {
                        Id = num++,
                        Species = entry.Value,
                    };

            this.Species = r.ToList();

            for (int index = 1; index < this.Species.Count; index++)
            {
                if (index - 1 < workspace.SpecDescFile.Entries.Count)
                {
                    this.Species[index].SpecDesc = workspace.SpecDescFile.Entries[index - 1];
                }

                if (index - 1 < workspace.ShipListFile.Entries.Count)
                {
                    this.Species[index].ShipList = workspace.ShipListFile.Entries[index - 1];
                }
            }

            foreach (var species in this.Species)
            {
                species.ModelName = workspace.GetModelName(species.Species);
            }

            this.Species[0].ModelName = "Do Not Use";
        }

        private void SetObjects(XwaWorkspace workspace)
        {
            int num = 0;
            var r = from entry in workspace.ObjectTable.Entries
                    select new ObjectEntry
                    {
                        Id = num++,
                        Object = entry,
                    };

            this.Objects = r.ToList();

            foreach (var obj in this.Objects)
            {
                obj.ModelName = workspace.GetModelName(obj.Object.DataIndex1, obj.Object.DataIndex2);
            }

            this.Objects[0].ModelName = "Do Not Use";
        }

        private void SetCrafts(XwaWorkspace workspace)
        {
            int num = 0;
            var r = from entry in workspace.CraftTable.Entries
                    select new CraftEntry
                    {
                        Id = num++,
                        Craft = entry
                    };

            this.Crafts = r.ToList();

            foreach (ObjectEntry obj in this.Objects)
            {
                short craftIndex = obj.Object.CraftIndex;

                if (craftIndex < 0 || craftIndex >= this.Crafts.Count)
                {
                    continue;
                }

                this.Crafts[craftIndex].Object = obj.Object;
            }

            foreach (var craft in this.Crafts)
            {
                if (craft.Object == null)
                {
                    continue;
                }

                craft.ModelName = workspace.GetModelName(craft.Object.DataIndex1, craft.Object.DataIndex2);
            }
        }
    }
}
