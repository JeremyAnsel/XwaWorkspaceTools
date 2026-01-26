using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JeremyAnsel.Xwa.Cbm;
using JeremyAnsel.Xwa.Workspace;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XwaWorkspaceEditor
{
    public sealed partial class ViewModel : ObservableObject
    {
        public sealed partial class ShipListEntry : ObservableObject
        {
            private readonly XwaShipListEntry ship;

            public ShipListEntry(XwaShipListEntry ship, ViewModel model)
            {
                this.ship = ship ?? new XwaShipListEntry();
                this.Model = model;
            }

            public XwaShipListEntry GetShip()
            {
                return this.ship;
            }

            public ViewModel Model { get; }

            public string CraftName
            {
                get
                {
                    return ship.CraftName;
                }

                set
                {
                    ship.CraftName = value;
                    OnPropertyChanged();
                }
            }

            public XwaShipListCraftType CraftType
            {
                get
                {
                    return ship.CraftType;
                }

                set
                {
                    ship.CraftType = value;
                    OnPropertyChanged();
                }
            }

            public XwaShipListFlyableOption Flyable
            {
                get
                {
                    return ship.Flyable;
                }

                set
                {
                    ship.Flyable = value;
                    OnPropertyChanged();
                }
            }

            public XwaShipListKnownOption Known
            {
                get
                {
                    return ship.Known;
                }

                set
                {
                    ship.Known = value;
                    OnPropertyChanged();
                }
            }

            public XwaShipListSkirmishOption Skirmish
            {
                get
                {
                    return ship.Skirmish;
                }

                set
                {
                    ship.Skirmish = value;
                    OnPropertyChanged();
                }
            }

            public int MapIconRectLeft
            {
                get
                {
                    return ship.MapIconRectLeft;
                }

                set
                {
                    ship.MapIconRectLeft = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MapIconRect));
                    OnPropertyChanged(nameof(MapIcon));
                }
            }

            public int MapIconRectTop
            {
                get
                {
                    return ship.MapIconRectTop;
                }

                set
                {
                    ship.MapIconRectTop = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MapIconRect));
                    OnPropertyChanged(nameof(MapIcon));
                }
            }

            public int MapIconRectRight
            {
                get
                {
                    return ship.MapIconRectRight;
                }

                set
                {
                    ship.MapIconRectRight = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MapIconRect));
                    OnPropertyChanged(nameof(MapIcon));
                }
            }

            public int MapIconRectBottom
            {
                get
                {
                    return ship.MapIconRectBottom;
                }

                set
                {
                    ship.MapIconRectBottom = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(MapIconRect));
                    OnPropertyChanged(nameof(MapIcon));
                }
            }

            public string MapIconRect
            {
                get
                {
                    return ship.MapIconRect;
                }
            }

            public BitmapSource[] MapIcon
            {
                get
                {
                    var sources = new BitmapSource[this.Model.Licons.Length];

                    var rect = new Int32Rect(
                                this.MapIconRectLeft,
                                this.MapIconRectTop,
                                this.MapIconRectRight - this.MapIconRectLeft,
                                this.MapIconRectBottom - this.MapIconRectTop);

                    if (rect.Width <= 0 || rect.Height <= 0)
                    {
                        return sources;
                    }

                    try
                    {
                        for (int i = 0; i < this.Model.Licons.Length; i++)
                        {
                            sources[i] = new CroppedBitmap(this.Model.Licons[i], rect);
                        }
                    }
                    catch
                    {
                    }

                    return sources;
                }
            }
        }

        public sealed partial class SpecDescEntry : ObservableObject
        {
            private readonly XwaSpecDescEntry desc;

            public SpecDescEntry(XwaSpecDescEntry desc)
            {
                this.desc = desc ?? new XwaSpecDescEntry();
            }

            public XwaSpecDescEntry GetDesc()
            {
                return this.desc;
            }

            public string CraftLongName
            {
                get
                {
                    return desc.CraftLongName;
                }

                set
                {
                    desc.CraftLongName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CraftAbbreviation));
                }
            }

            public string CraftAbbreviation
            {
                get
                {
                    return desc.CraftAbbreviation;
                }
            }

            public string Manufacturer
            {
                get
                {
                    return desc.Manufacturer;
                }

                set
                {
                    desc.Manufacturer = value;
                    OnPropertyChanged();
                }
            }

            public string Side
            {
                get
                {
                    return desc.Side;
                }

                set
                {
                    desc.Side = value;
                    OnPropertyChanged();
                }
            }

            public string Description
            {
                get
                {
                    return desc.Description;
                }

                set
                {
                    desc.Description = value;
                    OnPropertyChanged();
                }
            }

            public string Crew
            {
                get
                {
                    return desc.Crew;
                }

                set
                {
                    desc.Crew = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class CraftStringsEntry : ObservableObject
        {
            private readonly XwaCraftGenderEntry gender;
            private readonly XwaCraftPluralNameEntry pluralName;
            private readonly XwaCraftShortNameEntry shortName;

            public CraftStringsEntry(XwaCraftGenderEntry gender, XwaCraftPluralNameEntry pluralName, XwaCraftShortNameEntry shortName)
            {
                this.gender = gender ?? new XwaCraftGenderEntry();
                this.pluralName = pluralName ?? new XwaCraftPluralNameEntry();
                this.shortName = shortName ?? new XwaCraftShortNameEntry();
            }

            public XwaCraftGenderEntry GetGender()
            {
                return this.gender;
            }

            public XwaCraftPluralNameEntry GetPluralName()
            {
                return this.pluralName;
            }

            public XwaCraftShortNameEntry GetShortName()
            {
                return this.shortName;
            }

            public string GenderKey
            {
                get
                {
                    return gender.Key;
                }

                set
                {
                    gender.Key = value;
                    OnPropertyChanged();
                }
            }

            public XwaCraftGender CraftGender
            {
                get
                {
                    return gender.CraftGender;
                }

                set
                {
                    gender.CraftGender = value;
                    OnPropertyChanged();
                }
            }

            public string GenderCraftName
            {
                get
                {
                    return gender.CraftName;
                }

                set
                {
                    gender.CraftName = value;
                    OnPropertyChanged();
                }
            }

            public string PluralNameKey
            {
                get
                {
                    return pluralName.Key;
                }

                set
                {
                    pluralName.Key = value;
                    OnPropertyChanged();
                }
            }

            public string CraftPluralName
            {
                get
                {
                    return pluralName.CraftPluralName;
                }

                set
                {
                    pluralName.CraftPluralName = value;
                    OnPropertyChanged();
                }
            }

            public string ShortNameKey
            {
                get
                {
                    return shortName.Key;
                }

                set
                {
                    shortName.Key = value;
                    OnPropertyChanged();
                }
            }

            public string CraftShortName
            {
                get
                {
                    return shortName.CraftShortName;
                }

                set
                {
                    shortName.CraftShortName = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class SpeciesEntry : ObservableObject
        {
            private readonly XwaExeSpeciesEntry species;

            public SpeciesEntry(XwaExeSpeciesEntry species)
            {
                this.species = species ?? new XwaExeSpeciesEntry();
            }

            public XwaExeSpeciesEntry GetSpecies()
            {
                return this.species;
            }

            public short Value
            {
                get
                {
                    return species.Value;
                }

                set
                {
                    species.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class ObjectEntry : ObservableObject
        {
            private readonly XwaExeObjectEntry exeObject;

            public ObjectEntry(XwaExeObjectEntry exeObject)
            {
                this.exeObject = exeObject ?? new XwaExeObjectEntry();
            }

            public XwaExeObjectEntry GetExeObject()
            {
                return this.exeObject;
            }

            public XwaExeObjectEnableOptions EnableOptions
            {
                get
                {
                    return exeObject.EnableOptions;
                }

                set
                {
                    exeObject.EnableOptions = value;
                    OnPropertyChanged();
                }
            }

            public XwaExeObjectRessourceOptions RessourceOptions
            {
                get
                {
                    return exeObject.RessourceOptions;
                }

                set
                {
                    exeObject.RessourceOptions = value;
                    OnPropertyChanged();
                }
            }

            public XwaObjectCategory ObjectCategory
            {
                get
                {
                    return exeObject.ObjectCategory;
                }

                set
                {
                    exeObject.ObjectCategory = value;
                    OnPropertyChanged();
                }
            }

            public XwaShipCategory ShipCategory
            {
                get
                {
                    return exeObject.ShipCategory;
                }

                set
                {
                    exeObject.ShipCategory = value;
                    OnPropertyChanged();
                }
            }

            public uint ObjectSize
            {
                get
                {
                    return exeObject.ObjectSize;
                }

                set
                {
                    exeObject.ObjectSize = value;
                    OnPropertyChanged();
                }
            }

            public XwaExeObjectGameOptions GameOptions
            {
                get
                {
                    return exeObject.GameOptions;
                }

                set
                {
                    exeObject.GameOptions = value;
                    OnPropertyChanged();
                }
            }

            public short CraftIndex
            {
                get
                {
                    return exeObject.CraftIndex;
                }

                set
                {
                    exeObject.CraftIndex = value;
                    OnPropertyChanged();
                }
            }

            public short DataIndex1
            {
                get
                {
                    return exeObject.DataIndex1;
                }

                set
                {
                    exeObject.DataIndex1 = value;
                    OnPropertyChanged();
                }
            }

            public short DataIndex2
            {
                get
                {
                    return exeObject.DataIndex2;
                }

                set
                {
                    exeObject.DataIndex2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class WeaponEntry : ObservableObject
        {
            private readonly XwaExeWeaponEntry weapon;

            public WeaponEntry(XwaExeWeaponEntry weapon, int id)
            {
                this.weapon = weapon ?? new XwaExeWeaponEntry();
                this.Id = id;
            }

            public XwaExeWeaponEntry GetWeapon()
            {
                return this.weapon;
            }

            public int Id { get; }

            public int Power
            {
                get
                {
                    return weapon.Power;
                }

                set
                {
                    weapon.Power = value;
                    OnPropertyChanged();
                }
            }

            public short Speed
            {
                get
                {
                    return weapon.Speed;
                }

                set
                {
                    weapon.Speed = value;
                    OnPropertyChanged();
                }
            }

            public ushort DurationIntegerPart
            {
                get
                {
                    return weapon.DurationIntegerPart;
                }

                set
                {
                    weapon.DurationIntegerPart = value;
                    OnPropertyChanged();
                }
            }

            public ushort DurationDecimalPart
            {
                get
                {
                    return weapon.DurationDecimalPart;
                }

                set
                {
                    weapon.DurationDecimalPart = value;
                    OnPropertyChanged();
                }
            }

            public short HitboxSpan
            {
                get
                {
                    return weapon.HitboxSpan;
                }

                set
                {
                    weapon.HitboxSpan = value;
                    OnPropertyChanged();
                }
            }

            public byte Behavior
            {
                get
                {
                    return weapon.Behavior;
                }

                set
                {
                    weapon.Behavior = value;
                    OnPropertyChanged();
                }
            }

            public short Score
            {
                get
                {
                    return weapon.Score;
                }

                set
                {
                    weapon.Score = value;
                    OnPropertyChanged();
                }
            }

            public sbyte Side
            {
                get
                {
                    return weapon.Side;
                }

                set
                {
                    weapon.Side = value;
                    OnPropertyChanged();
                }
            }

            public short SideModel
            {
                get
                {
                    return weapon.SideModel;
                }

                set
                {
                    weapon.SideModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class CraftEntry : ObservableObject
        {
            private readonly XwaExeCraftEntry craft;

            public CraftEntry(XwaExeCraftEntry craft, int id)
            {
                this.craft = craft ?? new XwaExeCraftEntry();
                this.Id = id;
            }

            public XwaExeCraftEntry GetCraft()
            {
                return this.craft;
            }

            public int Id { get; }

            public short Score
            {
                get
                {
                    return craft.Score;
                }

                set
                {
                    craft.Score = value;
                    OnPropertyChanged();
                }
            }

            public short PromoPoints
            {
                get
                {
                    return craft.PromoPoints;
                }

                set
                {
                    craft.PromoPoints = value;
                    OnPropertyChanged();
                }
            }

            public bool HasHyperdrive
            {
                get
                {
                    return craft.HasHyperdrive;
                }

                set
                {
                    craft.HasHyperdrive = value;
                    OnPropertyChanged();
                }
            }

            public XwaGunConvergence GunConvergence
            {
                get
                {
                    return craft.GunConvergence;
                }

                set
                {
                    craft.GunConvergence = value;
                    OnPropertyChanged();
                }
            }

            public bool HasShieldGenerator
            {
                get
                {
                    return craft.HasShieldGenerator;
                }

                set
                {
                    craft.HasShieldGenerator = value;
                    OnPropertyChanged();
                }
            }

            public uint ShieldStrength
            {
                get
                {
                    return craft.ShieldStrength;
                }

                set
                {
                    craft.ShieldStrength = value;
                    OnPropertyChanged();
                }
            }

            public byte AIHitsTakenToEvade
            {
                get
                {
                    return craft.AIHitsTakenToEvade;
                }

                set
                {
                    craft.AIHitsTakenToEvade = value;
                    OnPropertyChanged();
                }
            }

            public uint HullStrength
            {
                get
                {
                    return craft.HullStrength;
                }

                set
                {
                    craft.HullStrength = value;
                    OnPropertyChanged();
                }
            }

            public uint CriticalDamageThreshold
            {
                get
                {
                    return craft.CriticalDamageThreshold;
                }

                set
                {
                    craft.CriticalDamageThreshold = value;
                    OnPropertyChanged();
                }
            }

            public ushort SystemStrength
            {
                get
                {
                    return craft.SystemStrength;
                }

                set
                {
                    craft.SystemStrength = value;
                    OnPropertyChanged();
                }
            }

            public byte EngineThrottle
            {
                get
                {
                    return craft.EngineThrottle;
                }

                set
                {
                    craft.EngineThrottle = value;
                    OnPropertyChanged();
                }
            }

            public short Speed
            {
                get
                {
                    return craft.Speed;
                }

                set
                {
                    craft.Speed = value;
                    OnPropertyChanged();
                }
            }

            public short Acceleration
            {
                get
                {
                    return craft.Acceleration;
                }

                set
                {
                    craft.Acceleration = value;
                    OnPropertyChanged();
                }
            }

            public short Deceleration
            {
                get
                {
                    return craft.Deceleration;
                }

                set
                {
                    craft.Deceleration = value;
                    OnPropertyChanged();
                }
            }

            public short Yaw
            {
                get
                {
                    return craft.Yaw;
                }

                set
                {
                    craft.Yaw = value;
                    OnPropertyChanged();
                }
            }

            public ushort YawPercent
            {
                get
                {
                    return craft.YawPercent;
                }

                set
                {
                    craft.YawPercent = value;
                    OnPropertyChanged();
                }
            }

            public short Roll
            {
                get
                {
                    return craft.Roll;
                }

                set
                {
                    craft.Roll = value;
                    OnPropertyChanged();
                }
            }

            public short Pitch
            {
                get
                {
                    return craft.Pitch;
                }

                set
                {
                    craft.Pitch = value;
                    OnPropertyChanged();
                }
            }

            public short DestroyRotation
            {
                get
                {
                    return craft.DestroyRotation;
                }

                set
                {
                    craft.DestroyRotation = value;
                    OnPropertyChanged();
                }
            }

            public short DriftSpeed
            {
                get
                {
                    return craft.DriftSpeed;
                }

                set
                {
                    craft.DriftSpeed = value;
                    OnPropertyChanged();
                }
            }

            public string CockpitFile
            {
                get
                {
                    return craft.CockpitFile;
                }

                set
                {
                    craft.CockpitFile = value;
                    OnPropertyChanged();
                }
            }

            public XwaExeCraftLaser[] Lasers
            {
                get
                {
                    return craft.Lasers;
                }
            }

            public XwaExeCraftWarhead[] Warheads
            {
                get
                {
                    return craft.Warheads;
                }
            }

            public byte CounterMeasuresCount
            {
                get
                {
                    return craft.CounterMeasuresCount;
                }

                set
                {
                    craft.CounterMeasuresCount = value;
                    OnPropertyChanged();
                }
            }

            public int CockpitDeltaY
            {
                get
                {
                    return craft.CockpitDeltaY;
                }

                set
                {
                    craft.CockpitDeltaY = value;
                    OnPropertyChanged();
                }
            }

            public short CockpitPositionX
            {
                get
                {
                    return craft.CockpitPositionX;
                }

                set
                {
                    craft.CockpitPositionX = value;
                    OnPropertyChanged();
                }
            }

            public short CockpitPositionY
            {
                get
                {
                    return craft.CockpitPositionY;
                }

                set
                {
                    craft.CockpitPositionY = value;
                    OnPropertyChanged();
                }
            }

            public short CockpitPositionZ
            {
                get
                {
                    return craft.CockpitPositionZ;
                }

                set
                {
                    craft.CockpitPositionZ = value;
                    OnPropertyChanged();
                }
            }

            public XwaExeCraftTurret[] Turrets
            {
                get
                {
                    return craft.Turrets;
                }
            }

            public int DockPositionY
            {
                get
                {
                    return craft.DockPositionY;
                }

                set
                {
                    craft.DockPositionY = value;
                    OnPropertyChanged();
                }
            }

            public int DockFromSmallPositionZ
            {
                get
                {
                    return craft.DockFromSmallPositionZ;
                }

                set
                {
                    craft.DockFromSmallPositionZ = value;
                    OnPropertyChanged();
                }
            }

            public int DockFromBigPositionZ
            {
                get
                {
                    return craft.DockFromBigPositionZ;
                }

                set
                {
                    craft.DockFromBigPositionZ = value;
                    OnPropertyChanged();
                }
            }

            public int DockToSmallPositionZ
            {
                get
                {
                    return craft.DockToSmallPositionZ;
                }

                set
                {
                    craft.DockToSmallPositionZ = value;
                    OnPropertyChanged();
                }
            }

            public int DockToBigPositionZ
            {
                get
                {
                    return craft.DockToBigPositionZ;
                }

                set
                {
                    craft.DockToBigPositionZ = value;
                    OnPropertyChanged();
                }
            }

            public int InsideHangarX
            {
                get
                {
                    return craft.InsideHangarX;
                }

                set
                {
                    craft.InsideHangarX = value;
                    OnPropertyChanged();
                }
            }

            public int InsideHangarZ
            {
                get
                {
                    return craft.InsideHangarZ;
                }

                set
                {
                    craft.InsideHangarZ = value;
                    OnPropertyChanged();
                }
            }

            public int InsideHangarY
            {
                get
                {
                    return craft.InsideHangarY;
                }

                set
                {
                    craft.InsideHangarY = value;
                    OnPropertyChanged();
                }
            }

            public int OutsideHangarX
            {
                get
                {
                    return craft.OutsideHangarX;
                }

                set
                {
                    craft.OutsideHangarX = value;
                    OnPropertyChanged();
                }
            }

            public int OutsideHangarZ
            {
                get
                {
                    return craft.OutsideHangarZ;
                }

                set
                {
                    craft.OutsideHangarZ = value;
                    OnPropertyChanged();
                }
            }

            public int OutsideHangarY
            {
                get
                {
                    return craft.OutsideHangarY;
                }

                set
                {
                    craft.OutsideHangarY = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class FlightModelSpacecraftEntry : ObservableObject
        {
            private readonly XwaFlightModelListEntry entry;

            public FlightModelSpacecraftEntry(XwaFlightModelListEntry entry)
            {
                this.entry = entry ?? new XwaFlightModelListEntry();
            }

            public XwaFlightModelListEntry GetEntry()
            {
                return this.entry;
            }

            public string Value
            {
                get
                {
                    return entry.Value;
                }

                set
                {
                    entry.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public sealed partial class FlightModelEquipmentEntry : ObservableObject
        {
            private readonly XwaFlightModelListEntry entry;

            public FlightModelEquipmentEntry(XwaFlightModelListEntry entry)
            {
                this.entry = entry ?? new XwaFlightModelListEntry();
            }

            public XwaFlightModelListEntry GetEntry()
            {
                return this.entry;
            }

            public string Value
            {
                get
                {
                    return entry.Value;
                }

                set
                {
                    entry.Value = value;
                    OnPropertyChanged();
                }
            }
        }

        public ViewModel()
        {
        }

        public ViewModel(string path)
            : this()
        {
            var workspace = new XwaWorkspace(path);

            this.WorkingDirectory = path;
            this.LoadLicons(workspace);
            this.SetShipLists(workspace);
            this.SetSpecDesc(workspace);
            this.SetCraftStrings(workspace);
            this.SetSpecies(workspace);
            this.SetObjects(workspace);
            this.SetCrafts(workspace);
            this.SetWeapons(workspace);
            this.SetXwaFlightModelListSpacecraft(workspace);
            this.SetXwaFlightModelListEquipment(workspace);
        }

        public void Refresh()
        {
            OnPropertyChanged("");
        }

        public string WorkingDirectory { get; set; }

        public BitmapSource[] Licons { get; private set; }

        public ObservableCollection<ShipListEntry> ShipLists { get; } = new();

        public ObservableCollection<SpecDescEntry> SpecDescs { get; } = new();

        public ObservableCollection<CraftStringsEntry> CraftStrings { get; } = new();

        public ObservableCollection<SpeciesEntry> Species { get; } = new();

        public ObservableCollection<ObjectEntry> Objects { get; } = new();

        public ObservableCollection<CraftEntry> Crafts { get; } = new();

        public ObservableCollection<WeaponEntry> Weapons { get; } = new();

        public ObservableCollection<FlightModelSpacecraftEntry> FlightModelSpacecraft { get; } = new();

        public ObservableCollection<FlightModelEquipmentEntry> FlightModelEquipment { get; } = new();

        private void LoadLicons(XwaWorkspace workspace)
        {
            try
            {
                this.Licons = new BitmapSource[6];
                this.Licons[0] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\lgrn_4.cbm"));
                this.Licons[1] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\lred_4.cbm"));
                this.Licons[2] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\lblu_4.cbm"));
                this.Licons[3] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\lyel_4.cbm"));
                this.Licons[4] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\lprp_4.cbm"));
                this.Licons[5] = CreateBitmapSourceFromCbmFile(Path.Combine(workspace.WorkingDirectory, @"FrontRes\MapIcons\Licons.cbm"));
            }
            catch
            {
            }
        }

        private void SetShipLists(XwaWorkspace workspace)
        {
            foreach (var model in workspace.ShipListFile.Entries)
            {
                var entry = new ShipListEntry(model, this);

                this.ShipLists.Add(entry);
            }
        }

        [RelayCommand]
        private void AddNewShipListEntry(ListBox selector)
        {
            if (selector is null || ShipLists.Count >= XwaShipListFile.MaxEntryCount)
            {
                return;
            }

            var entry = new ShipListEntry(null, this);
            ShipLists.Add(entry);
            selector.SelectedItem = entry;
            selector.ScrollIntoView(entry);
        }

        [RelayCommand]
        private void ClearShipListEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            ShipLists[selector.SelectedIndex] = new ShipListEntry(null, this);
        }

        [RelayCommand]
        private void RemoveShipListEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedItem is not ShipListEntry entry)
            {
                return;
            }

            if (MessageBox.Show($"Do you want to remove entry {selector.SelectedIndex + 1}: {entry.CraftName} ?", "Remove Spec entry", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            ShipLists.Remove(entry);

            CollectionViewSource
                .GetDefaultView(selector.ItemsSource)
                .Refresh();
        }

        private void SetSpecDesc(XwaWorkspace workspace)
        {
            foreach (var model in workspace.SpecDescFile.Entries)
            {
                var entry = new SpecDescEntry(model);

                this.SpecDescs.Add(entry);
            }
        }

        [RelayCommand]
        private void AddNewSpecDescEntry(ListBox selector)
        {
            if (selector is null || SpecDescs.Count >= XwaSpecDescFile.MaxEntryCount)
            {
                return;
            }

            var entry = new SpecDescEntry(null);
            SpecDescs.Add(entry);
            selector.SelectedItem = entry;
            selector.ScrollIntoView(entry);
        }

        [RelayCommand]
        private void ClearSpecDescEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            SpecDescs[selector.SelectedIndex] = new SpecDescEntry(null);
        }

        [RelayCommand]
        private void RemoveSpecDescEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedItem is not SpecDescEntry entry)
            {
                return;
            }

            if (MessageBox.Show($"Do you want to remove entry {selector.SelectedIndex + 1}: {entry.CraftLongName} ?", "Remove Desc entry", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            SpecDescs.Remove(entry);

            CollectionViewSource
                .GetDefaultView(selector.ItemsSource)
                .Refresh();
        }

        private void SetCraftStrings(XwaWorkspace workspace)
        {
            for (int index = 0; index < XwaCraftGenderFile.EntryCount; index++)
            {
                var gender = workspace.CraftGenderFile.Entries[index];
                var pluralName = workspace.CraftPluralNameFile.Entries[index];
                var shortName = workspace.CraftShortNameFile.Entries[index];

                var entry = new CraftStringsEntry(gender, pluralName, shortName);

                this.CraftStrings.Add(entry);
            }
        }

        [RelayCommand]
        private void ClearCraftStringsEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            CraftStrings[selector.SelectedIndex] = new CraftStringsEntry(null, null, null);
        }

        private void SetSpecies(XwaWorkspace workspace)
        {
            for (int index = 0; index < XwaExeSpeciesTable.EntryCount; index++)
            {
                var species = workspace.SpeciesTable.Entries[index];

                var entry = new SpeciesEntry(species);

                this.Species.Add(entry);
            }
        }

        [RelayCommand]
        private void ClearSpeciesEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            Species[selector.SelectedIndex] = new SpeciesEntry(null);
        }

        private void SetObjects(XwaWorkspace workspace)
        {
            for (int index = 0; index < XwaExeObjectTable.EntryCount; index++)
            {
                var exeObject = workspace.ObjectTable.Entries[index];

                var entry = new ObjectEntry(exeObject);

                this.Objects.Add(entry);
            }
        }

        [RelayCommand]
        private void ClearObjectEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            Objects[selector.SelectedIndex] = new ObjectEntry(null);
        }

        [RelayCommand]
        private void CreateShpFileForObjectEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            var shp = new XwaShpFile();

            ObjectEntry objectEntry = Objects.ElementAtOrDefault(selector.SelectedIndex);
            if (objectEntry is null)
            {
                return;
            }

            int speciesIndex = -1;
            for (int i = 0; i < Species.Count; i++)
            {
                if (Species[i].Value == selector.SelectedIndex)
                {
                    speciesIndex = i;
                    break;
                }
            }

            shp.ObjectIndex = (byte)selector.SelectedIndex;

            shp.ShipCategory = objectEntry.ShipCategory;
            shp.ObjectGameOptions = objectEntry.GameOptions;
            shp.OptFile = objectEntry.DataIndex1 switch
            {
                0 => FlightModelSpacecraft.ElementAtOrDefault(objectEntry.DataIndex2)?.Value ?? string.Empty,
                1 => FlightModelEquipment.ElementAtOrDefault(objectEntry.DataIndex2)?.Value ?? string.Empty,
                _ => string.Empty,
            };

            shp.Craft = Crafts.ElementAtOrDefault(objectEntry.CraftIndex)?.GetCraft() ?? new();

            CraftStringsEntry craftStringsEntry = CraftStrings.ElementAtOrDefault(selector.SelectedIndex - 1);
            if (craftStringsEntry is not null)
            {
                shp.CraftPluralName = craftStringsEntry.CraftPluralName;
                shp.CraftShortName = craftStringsEntry.CraftShortName;
            }

            SpecDescEntry specDescEntry = SpecDescs.ElementAtOrDefault(speciesIndex - 1);
            if (specDescEntry is not null)
            {
                shp.CraftLongName = specDescEntry.CraftLongName;
                shp.Manufacturer = specDescEntry.Manufacturer;
                shp.Side = specDescEntry.Side;
                shp.Crew = specDescEntry.Crew;
                shp.Description = specDescEntry.Description;
            }

            ShipListEntry shipListEntry = ShipLists.ElementAtOrDefault(speciesIndex - 1);
            if (shipListEntry is not null)
            {
                shp.Flyable = shipListEntry.Flyable;
                shp.CraftName = shipListEntry.CraftName;
            }

            var dialog = new SaveFileDialog();
            dialog.AddExtension = true;
            dialog.DefaultExt = ".shp";
            dialog.Filter = "SHP files (*.shp)|*.shp";
            dialog.InitialDirectory = this.WorkingDirectory;

            string fileName;

            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }

            try
            {
                shp.Write(fileName);
                MessageBox.Show($"{fileName} was created.", "Create SHP");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetCrafts(XwaWorkspace workspace)
        {
            for (int index = 0; index < XwaExeCraftTable.EntryCount; index++)
            {
                var craft = workspace.CraftTable.Entries[index];

                var entry = new CraftEntry(craft, index);

                this.Crafts.Add(entry);
            }
        }

        [RelayCommand]
        private void ClearCraftEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            Crafts[selector.SelectedIndex] = new CraftEntry(null, selector.SelectedIndex);
        }

        private void SetWeapons(XwaWorkspace workspace)
        {
            for (int index = 0; index < XwaExeWeaponTable.EntryCount; index++)
            {
                var weapon = workspace.WeaponTable.Entries[index];

                var entry = new WeaponEntry(weapon, 280 + index);

                this.Weapons.Add(entry);
            }
        }

        [RelayCommand]
        private void ClearWeaponEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            Weapons[selector.SelectedIndex] = new WeaponEntry(null, 280 + selector.SelectedIndex);
        }

        private void SetXwaFlightModelListSpacecraft(XwaWorkspace workspace)
        {
            foreach (var model in workspace.FlightModelSpacecraftFile.Entries)
            {
                var entry = new FlightModelSpacecraftEntry(model);

                this.FlightModelSpacecraft.Add(entry);
            }
        }

        [RelayCommand]
        private void AddNewFlightModelSpacecraftEntry(ListBox selector)
        {
            if (selector is null || FlightModelSpacecraft.Count >= XwaSpecDescFile.MaxEntryCount)
            {
                return;
            }

            var entry = new FlightModelSpacecraftEntry(null);
            FlightModelSpacecraft.Add(entry);
            selector.SelectedItem = entry;
            selector.ScrollIntoView(entry);
        }

        [RelayCommand]
        private void ClearFlightModelSpacecraftEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            FlightModelSpacecraft[selector.SelectedIndex] = new FlightModelSpacecraftEntry(null);
        }

        [RelayCommand]
        private void RemoveFlightModelSpacecraftEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedItem is not FlightModelSpacecraftEntry entry)
            {
                return;
            }

            if (MessageBox.Show($"Do you want to remove entry {selector.SelectedIndex}: {entry.Value} ?", "Remove Spacecraft entry", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            FlightModelSpacecraft.Remove(entry);

            CollectionViewSource
                .GetDefaultView(selector.ItemsSource)
                .Refresh();
        }

        private void SetXwaFlightModelListEquipment(XwaWorkspace workspace)
        {
            foreach (var model in workspace.FlightModelEquipmentFile.Entries)
            {
                var entry = new FlightModelEquipmentEntry(model);

                this.FlightModelEquipment.Add(entry);
            }
        }

        [RelayCommand]
        private void AddNewFlightModelEquipmentEntry(ListBox selector)
        {
            if (selector is null || FlightModelEquipment.Count >= XwaSpecDescFile.MaxEntryCount)
            {
                return;
            }

            var entry = new FlightModelEquipmentEntry(null);
            FlightModelEquipment.Add(entry);
            selector.SelectedItem = entry;
            selector.ScrollIntoView(entry);
        }

        [RelayCommand]
        private void ClearFlightModelEquipmentEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedIndex == -1)
            {
                return;
            }

            FlightModelEquipment[selector.SelectedIndex] = new FlightModelEquipmentEntry(null);
        }

        [RelayCommand]
        private void RemoveFlightModelEquipmentEntry(ListBox selector)
        {
            if (selector is null || selector.SelectedItem is not FlightModelEquipmentEntry entry)
            {
                return;
            }

            if (MessageBox.Show($"Do you want to remove entry {selector.SelectedIndex}: {entry.Value} ?", "Remove Equipment entry", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
            {
                return;
            }

            FlightModelEquipment.Remove(entry);

            CollectionViewSource
                .GetDefaultView(selector.ItemsSource)
                .Refresh();
        }

        public void Save()
        {
            var workspace = new XwaWorkspace(WorkingDirectory);

            workspace.ShipListFile.Entries.Clear();

            foreach (var entry in ShipLists)
            {
                workspace.ShipListFile.Entries.Add(entry.GetShip());
            }

            workspace.SpecDescFile.Entries.Clear();

            foreach (var entry in SpecDescs)
            {
                workspace.SpecDescFile.Entries.Add(entry.GetDesc());
            }

            workspace.CraftGenderFile.Entries.Clear();
            workspace.CraftPluralNameFile.Entries.Clear();
            workspace.CraftShortNameFile.Entries.Clear();

            foreach (var entry in CraftStrings)
            {
                workspace.CraftGenderFile.Entries.Add(entry.GetGender());
                workspace.CraftPluralNameFile.Entries.Add(entry.GetPluralName());
                workspace.CraftShortNameFile.Entries.Add(entry.GetShortName());
            }

            workspace.SpeciesTable.Entries.Clear();

            foreach (var entry in Species)
            {
                workspace.SpeciesTable.Entries.Add(entry.GetSpecies());
            }

            workspace.ObjectTable.Entries.Clear();

            foreach (var entry in Objects)
            {
                workspace.ObjectTable.Entries.Add(entry.GetExeObject());
            }

            workspace.CraftTable.Entries.Clear();

            foreach (var entry in Crafts)
            {
                workspace.CraftTable.Entries.Add(entry.GetCraft());
            }

            workspace.WeaponTable.Entries.Clear();

            foreach (var entry in Weapons)
            {
                workspace.WeaponTable.Entries.Add(entry.GetWeapon());
            }

            workspace.FlightModelSpacecraftFile.Entries.Clear();

            foreach (var entry in FlightModelSpacecraft)
            {
                workspace.FlightModelSpacecraftFile.Entries.Add(entry.GetEntry());
            }

            workspace.FlightModelEquipmentFile.Entries.Clear();

            foreach (var entry in FlightModelEquipment)
            {
                workspace.FlightModelEquipmentFile.Entries.Add(entry.GetEntry());
            }

            workspace.Write(workspace.WorkingDirectory);
        }

        private static BitmapSource CreateBitmapSourceFromCbmFile(string cbmPath)
        {
            CbmFile file = CbmFile.FromFile(cbmPath);
            CbmImage image = file.Images[0];
            byte[] data = image.GetImageData(true);
            return BitmapSource.Create(image.Width, image.Height, 96, 96, PixelFormats.Bgra32, null, data, image.Width * 4);
        }
    }
}
