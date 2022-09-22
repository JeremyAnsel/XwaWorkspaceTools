using JeremyAnsel.Xwa.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XwaWorkspaceViewer
{
    public sealed class ViewModel
    {
        public sealed class SpecEntry
        {
            public int Id { get; set; }

            public XwaSpecDescEntry Desc { get; set; }

            public XwaShipListEntry Ship { get; set; }
        }

        public sealed class CraftStringsEntry
        {
            public int Id { get; set; }

            public string GenderKey { get; set; }

            public XwaCraftGender CraftGender { get; set; }

            public string GenderCraftName { get; set; }

            public string PluralNameKey { get; set; }

            public string CraftPluralName { get; set; }

            public string ShortNameKey { get; set; }

            public string CraftShortName { get; set; }
        }

        public sealed class SpeciesEntry
        {
            public int Id { get; set; }

            public short Species { get; set; }
        }

        public sealed class ObjectEntry
        {
            public int Id { get; set; }

            public XwaExeObjectEntry Object { get; set; }
        }

        public sealed class CraftEntry
        {
            public int Id { get; set; }

            public XwaExeCraftEntry Craft { get; set; }

            public XwaExeObjectEntry Object { get; set; }
        }

        public sealed class WeaponEntry
        {
            public int Id { get; set; }

            public XwaExeWeaponEntry Weapon { get; set; }
        }

        public sealed class FlightModelSpacecraftEntry
        {
            public int Id { get; set; }

            public string Value { get; set; }
        }

        public sealed class FlightModelEquipmentEntry
        {
            public int Id { get; set; }

            public string Value { get; set; }
        }

        public ViewModel()
        {
        }

        public ViewModel(string path)
        {
            var workspace = new XwaWorkspace(path);

            this.WorkingDirectory = path;
            this.SetSpecs(workspace);
            this.SetCraftStrings(workspace);
            this.SetSpecies(workspace);
            this.SetObjects(workspace);
            this.SetCrafts(workspace);
            this.SetWeapons(workspace);
            this.SetXwaFlightModelListSpacecraft(workspace);
            this.SetXwaFlightModelListEquipment(workspace);
        }

        public string WorkingDirectory { get; set; }

        public List<SpecEntry> Specs { get; set; }

        public List<CraftStringsEntry> CraftStrings { get; set; }

        public List<SpeciesEntry> Species { get; set; }

        public List<ObjectEntry> Objects { get; set; }

        public List<CraftEntry> Crafts { get; set; }

        public List<WeaponEntry> Weapons { get; set; }

        public List<FlightModelSpacecraftEntry> FlightModelSpacecraft { get; set; }

        public List<FlightModelEquipmentEntry> FlightModelEquipment { get; set; }

        private void SetSpecs(XwaWorkspace workspace)
        {
            int count = Math.Max(
                workspace.SpecDescFile.Entries.Count,
                workspace.ShipListFile.Entries.Count);

            var specs = new List<SpecEntry>(count);

            for (int index = 0; index < count; index++)
            {
                var entry = new SpecEntry
                {
                    Id = index + 1,
                    Desc = workspace.SpecDescFile.Entries.ElementAtOrDefault(index) ?? new XwaSpecDescEntry(),
                    Ship = workspace.ShipListFile.Entries.ElementAtOrDefault(index) ?? new XwaShipListEntry()
                };

                specs.Add(entry);
            }

            this.Specs = specs;
        }

        private void SetCraftStrings(XwaWorkspace workspace)
        {
            int count = Math.Max(
                workspace.CraftGenderFile.Entries.Count,
                Math.Max(
                    workspace.CraftPluralNameFile.Entries.Count,
                    workspace.CraftShortNameFile.Entries.Count));

            var craftStrings = new List<CraftStringsEntry>(count);

            for (int index = 0; index < count; index++)
            {
                var craftGenderEntry = workspace.CraftGenderFile.Entries.ElementAtOrDefault(index) ?? new XwaCraftGenderEntry();
                var craftPluralNameEntry = workspace.CraftPluralNameFile.Entries.ElementAtOrDefault(index) ?? new XwaCraftPluralNameEntry();
                var craftShortNameEntry = workspace.CraftShortNameFile.Entries.ElementAtOrDefault(index) ?? new XwaCraftShortNameEntry();

                var entry = new CraftStringsEntry
                {
                    Id = index + 1,
                    GenderKey = craftGenderEntry.Key,
                    CraftGender = craftGenderEntry.CraftGender,
                    GenderCraftName = craftGenderEntry.CraftName,
                    PluralNameKey = craftPluralNameEntry.Key,
                    CraftPluralName = craftPluralNameEntry.CraftPluralName,
                    ShortNameKey = craftShortNameEntry.Key,
                    CraftShortName = craftShortNameEntry.CraftShortName
                };

                craftStrings.Add(entry);
            }

            this.CraftStrings = craftStrings;
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
        }

        private void SetWeapons(XwaWorkspace workspace)
        {
            int num = 280;
            var r = from entry in workspace.WeaponTable.Entries
                    select new WeaponEntry
                    {
                        Id = num++,
                        Weapon = entry,
                    };

            this.Weapons = r.ToList();
        }

        private void SetXwaFlightModelListSpacecraft(XwaWorkspace workspace)
        {
            int num = 0;
            var r = from entry in workspace.FlightModelSpacecraftFile.Entries
                    select new FlightModelSpacecraftEntry
                    {
                        Id = num++,
                        Value = entry.Value
                    };

            this.FlightModelSpacecraft = r.ToList();
        }

        private void SetXwaFlightModelListEquipment(XwaWorkspace workspace)
        {
            int num = 0;
            var r = from entry in workspace.FlightModelEquipmentFile.Entries
                    select new FlightModelEquipmentEntry
                    {
                        Id = num++,
                        Value = entry.Value
                    };

            this.FlightModelEquipment = r.ToList();
        }
    }
}
