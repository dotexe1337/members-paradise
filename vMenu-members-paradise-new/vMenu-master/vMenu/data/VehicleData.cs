using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using static CitizenFX.Core.Native.API;

namespace vMenuClient
{
    public static class VehicleData
    {
        public struct VehicleColor
        {
            public readonly int id;
            public readonly string label;

            public VehicleColor(int id, string label)
            {
                if (label == "veh_color_taxi_yellow")
                {
                    if (CitizenFX.Core.Native.API.GetLabelText("veh_color_taxi_yellow") == "NULL")
                    {
                        CitizenFX.Core.Native.API.AddTextEntry("veh_color_taxi_yellow", $"Taxi {CitizenFX.Core.Native.API.GetLabelText("IEC_T20_2")}");
                    }
                }
                else if (label == "veh_color_off_white")
                {
                    if (CitizenFX.Core.Native.API.GetLabelText("veh_color_off_white") == "NULL")
                    {
                        CitizenFX.Core.Native.API.AddTextEntry("veh_color_off_white", "Off White");
                    }
                }
                else if (label == "VERY_DARK_BLUE")
                {
                    if (CitizenFX.Core.Native.API.GetLabelText("VERY_DARK_BLUE") == "NULL")
                    {
                        CitizenFX.Core.Native.API.AddTextEntry("VERY_DARK_BLUE", "Very Dark Blue");
                    }
                }

                this.label = label;
                this.id = id;
            }
        }

        public static readonly List<VehicleColor> ClassicColors = new List<VehicleColor>()
        {
            new VehicleColor(0, "BLACK"),
            new VehicleColor(1, "GRAPHITE"),
            new VehicleColor(2, "BLACK_STEEL"),
            new VehicleColor(3, "DARK_SILVER"),
            new VehicleColor(4, "SILVER"),
            new VehicleColor(5, "BLUE_SILVER"),
            new VehicleColor(6, "ROLLED_STEEL"),
            new VehicleColor(7, "SHADOW_SILVER"),
            new VehicleColor(8, "STONE_SILVER"),
            new VehicleColor(9, "MIDNIGHT_SILVER"),
            new VehicleColor(10, "CAST_IRON_SIL"),
            new VehicleColor(11, "ANTHR_BLACK"),

            new VehicleColor(27, "RED"),
            new VehicleColor(28, "TORINO_RED"),
            new VehicleColor(29, "FORMULA_RED"),
            new VehicleColor(30, "BLAZE_RED"),
            new VehicleColor(31, "GRACE_RED"),
            new VehicleColor(32, "GARNET_RED"),
            new VehicleColor(33, "SUNSET_RED"),
            new VehicleColor(34, "CABERNET_RED"),
            new VehicleColor(35, "CANDY_RED"),
            new VehicleColor(36, "SUNRISE_ORANGE"),
            new VehicleColor(37, "GOLD"),
            new VehicleColor(38, "ORANGE"),

            new VehicleColor(49, "DARK_GREEN"),
            new VehicleColor(50, "RACING_GREEN"),
            new VehicleColor(51, "SEA_GREEN"),
            new VehicleColor(52, "OLIVE_GREEN"),
            new VehicleColor(53, "BRIGHT_GREEN"),
            new VehicleColor(54, "PETROL_GREEN"),

            new VehicleColor(61, "GALAXY_BLUE"),
            new VehicleColor(62, "DARK_BLUE"),
            new VehicleColor(63, "SAXON_BLUE"),
            new VehicleColor(64, "BLUE"),
            new VehicleColor(65, "MARINER_BLUE"),
            new VehicleColor(66, "HARBOR_BLUE"),
            new VehicleColor(67, "DIAMOND_BLUE"),
            new VehicleColor(68, "SURF_BLUE"),
            new VehicleColor(69, "NAUTICAL_BLUE"),
            new VehicleColor(70, "ULTRA_BLUE"),
            new VehicleColor(71, "PURPLE"),
            new VehicleColor(72, "SPIN_PURPLE"),
            new VehicleColor(73, "RACING_BLUE"),
            new VehicleColor(74, "LIGHT_BLUE"),

            new VehicleColor(88, "YELLOW"),
            new VehicleColor(89, "RACE_YELLOW"),
            new VehicleColor(90, "BRONZE"),
            new VehicleColor(91, "FLUR_YELLOW"),
            new VehicleColor(92, "LIME_GREEN"),

            new VehicleColor(94, "UMBER_BROWN"),
            new VehicleColor(95, "CREEK_BROWN"),
            new VehicleColor(96, "CHOCOLATE_BROWN"),
            new VehicleColor(97, "MAPLE_BROWN"),
            new VehicleColor(98, "SADDLE_BROWN"),
            new VehicleColor(99, "STRAW_BROWN"),
            new VehicleColor(100, "MOSS_BROWN"),
            new VehicleColor(101, "BISON_BROWN"),
            new VehicleColor(102, "WOODBEECH_BROWN"),
            new VehicleColor(103, "BEECHWOOD_BROWN"),
            new VehicleColor(104, "SIENNA_BROWN"),
            new VehicleColor(105, "SANDY_BROWN"),
            new VehicleColor(106, "BLEECHED_BROWN"),
            new VehicleColor(107, "CREAM"),

            new VehicleColor(111, "WHITE"),
            new VehicleColor(112, "FROST_WHITE"),

            new VehicleColor(135, "HOT PINK"),
            new VehicleColor(136, "SALMON_PINK"),
            new VehicleColor(137, "PINK"),
            new VehicleColor(138, "BRIGHT_ORANGE"),

            new VehicleColor(141, "MIDNIGHT_BLUE"),
            new VehicleColor(142, "MIGHT_PURPLE"),
            new VehicleColor(143, "WINE_RED"),

            new VehicleColor(145, "BRIGHT_PURPLE"),
            new VehicleColor(146, "VERY_DARK_BLUE"),
            new VehicleColor(147, "BLACK_GRAPHITE"),

            new VehicleColor(150, "LAVA_RED"),
        };

        public static readonly List<VehicleColor> MatteColors = new List<VehicleColor>()
        {
            new VehicleColor(12, "BLACK"),
            new VehicleColor(13, "GREY"),
            new VehicleColor(14, "LIGHT_GREY"),

            new VehicleColor(39, "RED"),
            new VehicleColor(40, "DARK_RED"),
            new VehicleColor(41, "ORANGE"),
            new VehicleColor(42, "YELLOW"),

            new VehicleColor(55, "LIME_GREEN"),

            new VehicleColor(82, "DARK_BLUE"),
            new VehicleColor(83, "BLUE"),
            new VehicleColor(84, "MIDNIGHT_BLUE"),

            new VehicleColor(128, "GREEN"),

            new VehicleColor(148, "Purple"),
            new VehicleColor(149, "MIGHT_PURPLE"),

            new VehicleColor(151, "MATTE_FOR"),
            new VehicleColor(152, "MATTE_OD"),
            new VehicleColor(153, "MATTE_DIRT"),
            new VehicleColor(154, "MATTE_DESERT"),
            new VehicleColor(155, "MATTE_FOIL"),
        };

        public static readonly List<VehicleColor> MetalColors = new List<VehicleColor>()
        {
            new VehicleColor(117, "BR_STEEL"),
            new VehicleColor(118, "BR BLACK_STEEL"),
            new VehicleColor(119, "BR_ALUMINIUM"),

            new VehicleColor(158, "GOLD_P"),
            new VehicleColor(159, "GOLD_S"),
        };

        public static readonly List<VehicleColor> UtilColors = new List<VehicleColor>()
        {
            new VehicleColor(15, "BLACK"),
            new VehicleColor(16, "FMMC_COL1_1"),
            new VehicleColor(17, "DARK_SILVER"),
            new VehicleColor(18, "SILVER"),
            new VehicleColor(19, "BLACK_STEEL"),
            new VehicleColor(20, "SHADOW_SILVER"),

            new VehicleColor(43, "DARK_RED"),
            new VehicleColor(44, "RED"),
            new VehicleColor(45, "GARNET_RED"),

            new VehicleColor(56, "DARK_GREEN"),
            new VehicleColor(57, "GREEN"),

            new VehicleColor(75, "DARK_BLUE"),
            new VehicleColor(76, "MIDNIGHT_BLUE"),
            new VehicleColor(77, "SAXON_BLUE"),
            new VehicleColor(78, "NAUTICAL_BLUE"),
            new VehicleColor(79, "BLUE"),
            new VehicleColor(80, "FMMC_COL1_13"),
            new VehicleColor(81, "BRIGHT_PURPLE"),

            new VehicleColor(93, "STRAW_BROWN"),

            new VehicleColor(108, "UMBER_BROWN"),
            new VehicleColor(109, "MOSS_BROWN"),
            new VehicleColor(110, "SANDY_BROWN"),

            new VehicleColor(122, "veh_color_off_white"),

            new VehicleColor(125, "BRIGHT_GREEN"),

            new VehicleColor(127, "HARBOR_BLUE"),

            new VehicleColor(134, "FROST_WHITE"),

            new VehicleColor(139, "LIME_GREEN"),
            new VehicleColor(140, "ULTRA_BLUE"),

            new VehicleColor(144, "GREY"),

            new VehicleColor(157, "LIGHT_BLUE"),

            new VehicleColor(160, "YELLOW")
        };

        public static readonly List<VehicleColor> WornColors = new List<VehicleColor>()
        {
            new VehicleColor(21, "BLACK"),
            new VehicleColor(22, "GRAPHITE"),
            new VehicleColor(23, "LIGHT_GREY"),
            new VehicleColor(24, "SILVER"),
            new VehicleColor(25, "BLUE_SILVER"),
            new VehicleColor(26, "SHADOW_SILVER"),

            new VehicleColor(46, "RED"),
            new VehicleColor(47, "SALMON_PINK"),
            new VehicleColor(48, "DARK_RED"),

            new VehicleColor(58, "DARK_GREEN"),
            new VehicleColor(59, "GREEN"),
            new VehicleColor(60, "SEA_GREEN"),

            new VehicleColor(85, "DARK_BLUE"),
            new VehicleColor(86, "BLUE"),
            new VehicleColor(87, "LIGHT_BLUE"),

            new VehicleColor(113, "SANDY_BROWN"),
            new VehicleColor(114, "BISON_BROWN"),
            new VehicleColor(115, "CREEK_BROWN"),
            new VehicleColor(116, "BLEECHED_BROWN"),

            new VehicleColor(121, "veh_color_off_white"),

            new VehicleColor(123, "ORANGE"),
            new VehicleColor(124, "SUNRISE_ORANGE"),

            new VehicleColor(126, "veh_color_taxi_yellow"),

            new VehicleColor(129, "RACING_GREEN"),
            new VehicleColor(130, "ORANGE"),
            new VehicleColor(131, "WHITE"),
            new VehicleColor(132, "FROST_WHITE"),
            new VehicleColor(133, "OLIVE_GREEN"),
        };

        public static class Vehicles
        {
            #region Vehicle List Per Class

            #region Example
            public static List<string> Example { get; } = new List<string>()
            {
                
            };
            #endregion

            #region Boats
            public static List<string> Boats { get; } = new List<string>()
            {
                "james",
                "gfboat",
                "victory",
                "bahia",
                "redja",
                "rboat",
                "sr650fly",
                "yacht2",
                "missouri",
                "yacht4",
            };
            #endregion

            #region Motorcycles
            public static List<string> Motorcycles { get; } = new List<string>()
            {
                "apd750",
                "aprsv4",
                "rsv4",
                "aprt",
                "bimota",
                "bmws",
                "bmwrp",
                "bmwsr",
                "bmwsrn",
                "d99",
                "ddc",
                "ddrr",
                "dgp15",
                "dgp215",
                "dhpm",
                "hdfb",
                "hdkn",
                "hdss",
                "hbros",
                "hcb18",
                "hcb1000",
                "hcbb",
                "hcbcr",
                "hf150",
                "hlcr",
                "hmarc",
                "hnsr",
                "honcb",
                "hcbr600",
                "honcbr",
                "hcbr17",
                "cbrrr",
                "hor14",
                "hor600",
                "hrc213",
                "hrcp213",
                "hxre",
                "hsmr",
                "hsmx",
                "hsmxe",
                "hn900",
                "hfc250",
                "ke400",
                "kgpz",
                "knh2",
                "knh22",
                "zx10",
                "knzx",
                "kx450f",
                "kz750",
                "kz800",
                "kz1000",
                "kza1000",
                "ktmpit",
                "ktmrc",
                "ktmrc8",
                "ktmtrr",
                "mvab",
                "mvaf",
                "mvar",
                "nbike",
                "p51",
                "sban",
                "gsxr",
                "srmz",
                "srmz2",
                "sxf450",
                "tmrs",
                "xj6",
                "yfz6",
                "yfz8",
                "yfz68",
                "ym1",
                "yml1",
                "fz07",
                "ymt10",
                "r3",
                "r6",
                "yr1",
                "yr15",
                "r1",
                "yss",
                "ytmax",
                "yx660",
                "yxj6",
                "yxt",
                "yzf",
            };
            #endregion

            #region Vanilla
            public static List<string> Vanilla { get; } = new List<string>()
            {
                "eurosle",
                "majimagt",
                "requiemzr1",
                "rh82",
                "savestrare",
                "stratumc",
                "zr",
                "spritzer",
                "schwarzer2",
                "bosozokupcj",
                "bosozokuglendale",
                "bosozokuprimo",
                "vesper",
                "nexus",
                "elegyrh5",
                "paragonxr",
                "turismoc",
                "futoS",
                "sultan2c",
                "sultanrsv8",
                "argento",
                "torerod",
                "meteor",
                "mf1",
                "sentinel6str2",
                "fuknking",
                "pretender",
                "squaddie2",
                "squaddie3",
                "stanier5",
                "Tstanced",
                "carbonizzare2",
                "tampac",
                "glendalelimo",
                "gcart",
                "primoard",
                "ziongtc",
                "voodoo3",
                "sultanrsstanced",
                "intruderstanced",
            };
            #endregion

            #region International
            public static List<string> International { get; } = new List<string>()
            {
                "flatbed4",
            };
            #endregion

            #region Five-O
            public static List<string> Five_O { get; } = new List<string>()
            {
                "crownvicpol",
                "chargerpol",
                "polzonda",
            };
            #endregion

            #region Acura
            public static List<string> Acura { get; } = new List<string>()
            {
                "integratyper",
                "04acuratsx",
            };
            #endregion

            #region Aston Martin
            public static List<string> Aston_Martin { get; } = new List<string>()
            {
                "rapide",
                "amdbx",
                "db11",
            };
            #endregion

            #region Audi
            public static List<string> Audi { get; } = new List<string>()
            {
                "rs72020",
                "q820",
                "r820",
                "rs318",
                "rs4avant",
                "rs5r",
                "rs6",
                "rs62",
                "rs7",
            };
            #endregion

            #region BMW
            public static List<string> BMW { get; } = new List<string>()
            {
                "328i",
                "bmw507",
                "b12lang",
                "b12langsh",
                "blze30",
                "m3e36",
                "e36t",
                "m3e36",
                "e46",
                "m3tp",
                "bme6tun",
                "e92lb",
                "m3f80",
                "f82",
                "m5f90",
                "m82020",
                "z4alchemist",
                "645ci",
                "e34touring",
                "e23",
                "M760Li",
                "z4",
            };
            #endregion

            #region Bentley
            public static List<string> Bentley { get; } = new List<string>()
            {
                "contgt13",
            };
            #endregion

            #region Bugatti
            public static List<string> Bugatti { get; } = new List<string>()
            {
                "bugatticentodieci",
                "chiron17",
                "bcss",
                "b164",
                "supersport",
            };
            #endregion

            #region Chevrolet
            public static List<string> Chevrolet { get; } = new List<string>()
            {
                "c8",
                "belair57",
                "elcamino70",
                "impaladonk",
                "01duramaxk",
                "86k30",
                "863500",
                "czr2",
                "proc10",
                "silverado",
                "cobaltss",
                "czr1",
            };
            #endregion

            #region Dodge
            public static List<string> Dodge { get; } = new List<string>()
            {
                "16Charger",
                "16Challenger",
                "Lancer57",
                "SRT2018",
                "99viper",
            };
            #endregion

            #region Ferrari
            public static List<string> Ferrari { get; } = new List<string>()
            {
                "yFe458i1",
                "yFe458i2",
                "yFe458s1",
                "yFe458s2",
                "pd458wb",
                "4881",
                "488misha",
                "ferrari812",
                "f430s",
                "f8t",
                "gtc4",
                "laferrari17",
                "monza",
            };
            #endregion

            #region Ford
            public static List<string> Ford { get; } = new List<string>()
            {
                "gt2017",
                "13fmb302",
                "raptor2017",
                "04f250k",
                "econoline",
                "f350",
                "f-one49d",
                "f-six49",
                "frr",
                "Raptor150",
                "richobs",
                "x6r",
            };
            #endregion

            #region GMC
            public static List<string> GMC { get; } = new List<string>()
            {
                "gmck",
                "gmc1500",
                "g20c",
            };
            #endregion

            #region Henry Clay
            public static List<string> Henry_Clay { get; } = new List<string>()
            {
                "gts8",
            };
            #endregion

            #region Honda
            public static List<string> Honda { get; } = new List<string>()
            {
                "acty",
                "ef9",
                "eg6pandem",
                "eg6",
                "ek9",
                "civic",
                "em1civic",
                "ep3",
                "fd2",
                "fk8",
                "city85",
                "crxdelsol",
                "crx2",
                "majcrx",
                "crz",
                "dc2",
                "dc5",
                "motoc",
                "na1",
                "nc1",
                "prelude",
                "prelude80",
                "integrabomex",
                "ap2",
                "honody",
            };
            #endregion

            #region Hyundai
            public static List<string> Hyundai { get; } = new List<string>()
            {
                "genesiscoupe",
                "genesiscoupestanced",
            };
            #endregion

            #region Infiniti
            public static List<string> Infiniti { get; } = new List<string>()
            {
                "ipl",
                "infg35",
            };
            #endregion

            #region Jeep
            public static List<string> Jeep { get; } = new List<string>()
            {
                "rubi3d",
                "willyshotrod",
                "seminole2",
            };
            #endregion

            #region Kia
            public static List<string> Kia { get; } = new List<string>()
            {
                "fortek",
                "mstinger",
            };
            #endregion

            #region Koenigsegg
            public static List<string> Koenigsegg { get; } = new List<string>()
            {
                "agera2011",
                "agerars",
                "jes",
            };
            #endregion

            #region Lamborghini
            public static List<string> Lamborghini { get; } = new List<string>()
            {
                "500gtrlam",
                "lp570",
                "lbsihu",
                "18performante",
                "sc18",
                "rmodsian",
                "lp670sv",
                "610dtm",
            };
            #endregion

            #region Lexus
            public static List<string> Lexus { get; } = new List<string>()
            {
                "gs300",
                "gs300wagon",
                "ISF",
                "89ls400",
                "ls430",
                "lc500",
                "lexy",
                "lexsc430",
            };
            #endregion

            #region Lincoln
            public static List<string> Lincoln { get; } = new List<string>()
            {
                "lincolncont",
            };
            #endregion

            #region Maserati
            public static List<string> Maserati { get; } = new List<string>()
            {
                "masgt",
            };
            #endregion

            #region Mazda
            public static List<string> Mazda { get; } = new List<string>()
            {
                "yata",
                "mazlex",
                "mxrb",
                "rx3",
                "fb",
                "majfc",
                "fc3s",
                "majfd",
                "fd",
                "rx7cwest",
                "rx7veilside",
                "rx7kazama",
                "rx811",
                "camberghini",
            };
            #endregion

            #region McLaren
            public static List<string> McLaren { get; } = new List<string>()
            {
                "675lt",
                "720s",
                "mcst",
            };
            #endregion

            #region Mercedes Benz
            public static List<string> Mercedes_Benz { get; } = new List<string>()
            {
                "190e",
                "mb300sl",
                "mbhome",
                "hometrailer",
                "w202",
                "e300",
                "e400",
                "e63s",
                "c63s",
                "gle450",
                "amggt63s",
                "w222s500",
                "slr",
                "Yachting",
                "e63amg",
            };
            #endregion

            #region Mini
            public static List<string> Mini { get; } = new List<string>()
            {
                "minilb",
            };
            #endregion

            #region Mitsubishi
            public static List<string> Mitsubishi { get; } = new List<string>()
            {
                "evo8",
                "evoviii",
                "evovaris",
                "mlec",
                "fq400",
            };
            #endregion

            #region Nissan
            public static List<string> Nissan { get; } = new List<string>()
            {
                "nis180",
                "180326",
                "z31",
                "z32",
                "350gt",
                "maj350",
                "nzp",
                "y33",
                "95y33",
                "Y32cedric",
                "s30",
                "c33",
                "180sx",
                "nis13",
                "s13cambered",
                "silvia",
                "s13onev",
                "majs14z",
                "nis15",
                "s15",
                "s15vert",
                "skylinec110",
                "2000gtr",
                "r30rsx",
                "r31",
                "nisr32",
                "hcr32",
                "nisr33",
                "skyline",
                "stagea34",
                "er34",
                "r35",
                "gtrp",
                "gtrlb2",
                "gtrsilhouette",
                "gtrc",
                "gtrcw",
                "nissantitan17",
                "180sxstanced",
                "s15stanced",
                "s15stanced2",
                "z32stanced",
                "y34gloria",
                "leopard",
                "cima1",
                "cima2",
            };
            #endregion

            #region Opel
            public static List<string> Opel { get; } = new List<string>()
            {
                "opeladam",
            };
            #endregion

            #region Pagani
            public static List<string> Pagani { get; } = new List<string>()
            {
                "huayrar",
            };
            #endregion

            #region Polaris
            public static List<string> Polaris { get; } = new List<string>()
            {
                "polaris",
            };
            #endregion

            #region Porsche
            public static List<string> Porsche { get; } = new List<string>()
            {
                "rikorwd",
                "992c",
                "992t",
                "str20",
                "3lb",
                "930mnc",
                "oldnew",
                "ursa",
                "caymanrb",
            };
            #endregion

            #region RAM
            public static List<string> RAM { get; } = new List<string>()
            {
                "19ramdonk",
                "19ramoffroad",
                "10ram",
                "2ndgendually",
                "megaramcustom",
                "runner",
            };
            #endregion

            #region Range Rover
            public static List<string> Range_Rover { get; } = new List<string>()
            {
                "lrrr",
                "evoque",
            };
            #endregion

            #region Renault
            public static List<string> Renault { get; } = new List<string>()
            {
                "twizy",
                "tondar",
            };
            #endregion

            #region Rolls Royce
            public static List<string> Rolls_Royce { get; } = new List<string>()
            {
                "dawn",
                "silver67",
            };
            #endregion

            #region Saleen
            public static List<string> Saleen { get; } = new List<string>()
            {
                "S1",
            };
            #endregion

            #region Skoda
            public static List<string> Skoda { get; } = new List<string>()
            {
                "17octavia",
            };
            #endregion

            #region Subaru
            public static List<string> Subaru { get; } = new List<string>()
            {
                "subwrx",
                "subisti08",
                "sti",
                "legacystanced",
            };
            #endregion

            #region TVR
            public static List<string> TVR { get; } = new List<string>()
            {
                "T18",
            };
            #endregion

            #region Toyota
            public static List<string> Toyota { get; } = new List<string>()
            {
                "maltezza",
                "maj86",
                "cam08",
                "celica",
                "celisupra",
                "celgt4",
                "levin86",
                "jzs175",
                "toy86",
                "yae86",
                "gx71",
                "gx81",
                "mk2100",
                "jzx100snowdrift",
                "majsoar",
                "jza70",
                "a80",
                "supraa90",
                "verossa",
                "avanza",
                "hiluxarctic",
                "streetsupra",
                "hiacesc",
                "mark2x90",
                "celsior1",
                "celsior2",
                "crownathlete",
                "alphard",
                "camry18",
                "prius17",
            };
            #endregion

            #region UAZ
            public static List<string> UAZ { get; } = new List<string>()
            {
                "uaz3159",
            };
            #endregion

            #region Volkswagen
            public static List<string> Volkswagen { get; } = new List<string>()
            {
                "mk3",
                "63lb",
                "passatsedan",
                "passatr",
                "sciroccos81",
                "r32",
            };
            #endregion

            public static Dictionary<string, List<string>> VehicleClasses { get; } = new Dictionary<string, List<string>>()
            {
                ["Boats"] = Boats,
                ["Motorcycles"] = Motorcycles,
                ["Vanilla"] = Vanilla,
                ["International"] = International,
                ["Five-O"] = Five_O,
                ["Acura"] = Acura,
                ["Aston Martin"] = Aston_Martin,
                ["Audi"] = Audi,
                ["BMW"] = BMW,
                ["Bentley"] = Bentley,
                ["Bugatti"] = Bugatti,
                ["Chevrolet"] = Chevrolet,
                ["Dodge"] = Dodge,
                ["Ferrari"] = Ferrari,
                ["Ford"] = Ford,
                ["GMC"] = GMC,
                ["Henry Clay"] = Henry_Clay,
                ["Honda"] = Honda,
                ["Hyundai"] = Hyundai,
                ["Infiniti"] = Infiniti,
                ["Jeep"] = Jeep,
                ["Kia"] = Kia,
                ["Koenigsegg"] = Koenigsegg,
                ["Lamborghini"] = Lamborghini,
                ["Lexus"] = Lexus,
                ["Lincoln"] = Lincoln,
                ["Maserati"] = Maserati,
                ["Mazda"] = Mazda,
                ["McLaren"] = McLaren,
                ["Mercedes Benz"] = Mercedes_Benz,
                ["Mini"] = Mini,
                ["Mitsubishi"] = Mitsubishi,
                ["Nissan"] = Nissan,
                ["Opel"] = Opel,
                ["Pagani"] = Pagani,
                ["Polaris"] = Polaris,
                ["Porsche"] = Porsche,
                ["RAM"] = RAM,
                ["Range Rover"] = Range_Rover,
                ["Renault"] = Renault,
                ["Rolls Royce"] = Rolls_Royce,
                ["Saleen"] = Saleen,
                ["Skoda"] = Skoda,
                ["Subaru"] = Subaru,
                ["TVR"] = TVR,
                ["Toyota"] = Toyota,
                ["UAZ"] = UAZ,
                ["Volkswagen"] = Volkswagen,
            };
            #endregion

            public static string[] GetAllVehicles()
            {
                List<string> vehs = new List<string>();
                foreach (var vc in VehicleClasses)
                {
                    foreach (var c in vc.Value)
                    {
                        vehs.Add(c);
                    }
                }
                return vehs.ToArray();
            }
        }
    }
}