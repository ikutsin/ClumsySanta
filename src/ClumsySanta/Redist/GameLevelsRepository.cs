using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ClumsySanta.Model;
using ClumsySanta.ViewModel;

namespace ClumsySanta.Redist
{
	public class GameLevelsRepository
	{
		public GameLevelModel GetLevelsAt(int index)
		{
			return GetAllLevels()[index];
		}

		public IList<GameLevelModel> GetAllLevels()
		{
			#region barbie presents

			int pox = 0, poy = 0;
			var gamePresentViewModelsForBarbie = new List<GamePresentViewModel>
			{

				new GamePresentViewModel
				{
					Name = "presents_03.png",
					Size = new Size(50, 36),
					Coordinate = new Point(1771, 201),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_03.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_05.png",
					Size = new Size(54, 36),
					Coordinate = new Point(1850 - pox, 201 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_05.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift01",
					Size = new Size(66, 44),
					Coordinate = new Point(1705 - pox, 280 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_10.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift02",
					Size = new Size(60, 37),
					Coordinate = new Point(1898 - pox, 280 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_12.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift03",
					Size = new Size(55, 37),
					Coordinate = new Point(2073 - pox, 280 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_14.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift04",
					Size = new Size(50, 41),
					Coordinate = new Point(1891 - pox, 398 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_20.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_24.png",
					Size = new Size(45, 36),
					Coordinate = new Point(1082 - pox, 454 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_24.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_27.png",
					Size = new Size(42, 35),
					Coordinate = new Point(963 - pox, 461 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_27.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift05",
					Size = new Size(57, 41),
					Coordinate = new Point(1648 - pox, 513 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_32.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift06",
					Size = new Size(52, 41),
					Coordinate = new Point(1737 - pox, 513 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_34.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift07",
					Size = new Size(49, 41),
					Coordinate = new Point(1821 - pox, 513 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_36.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift08",
					Size = new Size(54, 41),
					Coordinate = new Point(1904 - pox, 513 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_38.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift09",
					Size = new Size(50, 41),
					Coordinate = new Point(2067 - pox, 513 - poy),
					Level = "LaterPresentsCanvas",
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_40.png",
				},
//new GamePresentViewModel{
//Name = "PresentUnderBook",
//Size = new Size(105, 83),
//Coordinate = new Point(627-pox, 554-poy),
//Level = "LaterPresentsCanvas",
//BaseImage = @"Assets\GameEngine\Presents\scene2\presents_48.png",
//},
				new GamePresentViewModel
				{
					Name = "SkafGift11",
					Size = new Size(53, 47),
					Coordinate = new Point(1729 - pox, 590 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_51.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_54.png",
					Size = new Size(45, 39),
					Coordinate = new Point(1223 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_54.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift10",
					Size = new Size(50, 39),
					Coordinate = new Point(1648 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_56.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift12",
					Size = new Size(49, 39),
					Coordinate = new Point(1821 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_58.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift13",
					Size = new Size(50, 39),
					Coordinate = new Point(1898 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_60.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift14",
					Size = new Size(46, 49),
					Coordinate = new Point(1986 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_62.png",
				},
				new GamePresentViewModel
				{
					Name = "SkafGift15",
					Size = new Size(47, 49),
					Level = "LaterPresentsCanvas",
					Coordinate = new Point(2081 - pox, 598 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_64.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_66.png",
					Size = new Size(47, 41),
					Coordinate = new Point(1338 - pox, 606 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_66.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_68.png",
					Size = new Size(51, 41),
					Coordinate = new Point(1564 - pox, 606 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_68.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_71.png",
					Size = new Size(49, 43),
					Coordinate = new Point(906 - pox, 637 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_71.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_84.png",
					Size = new Size(57, 51),
					Coordinate = new Point(1813 - pox, 662 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_84.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_86.png",
					Size = new Size(58, 51),
					Coordinate = new Point(2015 - pox, 662 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_86.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_89.png",
					Size = new Size(52, 54),
					Coordinate = new Point(827 - pox, 676 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_89.png",
				},
				new GamePresentViewModel
				{
					Name = "PresentOnCarpet",
					Size = new Size(55, 48),
					Coordinate = new Point(1385 - pox, 689 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_92.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_99.png",
					Size = new Size(70, 64),
					Coordinate = new Point(943 - pox, 778 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_99.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_102.png",
					Size = new Size(72, 63),
					Coordinate = new Point(515 - pox, 812 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_102.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_106.png",
					Size = new Size(75, 74),
					Coordinate = new Point(1280 - pox, 851 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_106.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_110.png",
					Size = new Size(95, 81),
					Level = "LaterPresentsCanvas",
					Coordinate = new Point(2532 - pox, 917 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_110.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_112.png",
					Size = new Size(88, 90),
					Coordinate = new Point(827 - pox, 931 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_112.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_115.png",
					Size = new Size(102, 93),
					Coordinate = new Point(1603 - pox, 944 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_115.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_117.png",
					Size = new Size(107, 92),
					Level = "LaterPresentsCanvas",
					Coordinate = new Point(2185 - pox, 955 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_117.png",
				},
				new GamePresentViewModel
				{
					Name = "presents_121.png",
					Size = new Size(108, 100),
					Coordinate = new Point(143 - pox, 1021 - poy),
					BaseImage = @"Assets\GameEngine\Presents\scene2\presents_121.png",
				},
			};

			#endregion

			return new[]
			{
				#region level Bedroom new

				new GameLevelModel
				{
					PresentsToShow = 8,
					BackgroundImage = @"Assets\GameEngine\scene4_01.jpg",
					LaterBackgroundImage = @"Assets\GameEngine\scene4_02.jpg",
					BgImgSize = new Size(2500, 1406),
					StartPoint = new Point(650, 350),
					Name = "Bedroom",
					LevelPickerImage = @"Assets\ChooseLevel\bedroom.jpg",
					IsAvailableToPlay = true,
					PresentIcon = @"Assets\GameEngine\Presents\s4_gift.png",
					GrayPresentIcon = @"Assets\GameEngine\Presents\s4_gift_grey.png",
					PlaybackTime = TimeSpan.FromSeconds(20),
					Presents = new ObservableCollection<GamePresentViewModel>(new List<GamePresentViewModel>
					{
						new GamePresentViewModel
						{
							Name = "scene3_03.png", Size = new Size(39, 36), Coordinate = new Point(515, 406),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_03.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_10.png", Size = new Size(144, 66), Coordinate = new Point(130, 710),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_10.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_13.png", Size = new Size(49, 38), Coordinate = new Point(2033, 736),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_13.png",
							DependsOn = new ObservableCollection<string>(new[] { "scene4_13.png" })
						},
						new GamePresentViewModel
						{
							Name = "scene3_18.png", Size = new Size(73, 65), Coordinate = new Point(2010, 798),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_18.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_21.png", Size = new Size(81, 61), Coordinate = new Point(166, 801),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_21.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_24.png", Size = new Size(142, 131), Coordinate = new Point(1614, 817),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_24.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_27.png", Size = new Size(58, 48), Coordinate = new Point(792, 826),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_27.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_35.png", Size = new Size(80, 71), Coordinate = new Point(2000, 950),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_35.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_38.png", Size = new Size(79, 64), Coordinate = new Point(168, 955),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_38.png"
						},
						new GamePresentViewModel
						{
							Name = "scene3_40.png", Size = new Size(124, 119), Coordinate = new Point(1668, 1016),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_40.png",
							DependsOn = new ObservableCollection<string>(new[] { "elka" })
						},
						new GamePresentViewModel
						{
							Name = "scene3_45.png", Size = new Size(185, 150), Coordinate = new Point(2192, 1088),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_45.png",
							DependsOn = new ObservableCollection<string>(new[] { "elka" })
						},
						new GamePresentViewModel
						{
							Name = "scene3_48.png", Size = new Size(186, 106), Coordinate = new Point(1899, 1100),
							BaseImage = @"Assets\\GameEngine\\Presents\\scene4\\scene3_48.png",
							DependsOn = new ObservableCollection<string>(new[] { "elka" })
						},
					}),
					Decorations = new ObservableCollection<GameDecorationViewModel>(new List<GameDecorationViewModel>
					{
						new GameDecorationViewModel
						{
							Name = "aliska", SpecialSound = "squeek2", Size = new Size(384, 247), Coordinate = new Point(796, 297),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene2_03.png"
						},
						new GameDecorationViewModel
						{
							Name = "garderob", SpecialAnimation = "FadeOut", Size = new Size(364, 615), Coordinate = new Point(1492, 377),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene2_06.png", Level = "FirstDecorationsCanvas",
							DependsOn = new ObservableCollection<string>(new[] { "scene4_03.png", "scene4_04.png", "scene3_24.png" })
						},
						new GameDecorationViewModel
						{
							Name = "levaja tumba", Size = new Size(230, 290), Coordinate = new Point(95, 771),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene2_10.png", Level = "FirstDecorationsCanvas",
							DependsOn = new ObservableCollection<string>(new[]
								{ "scene4_17.png", "scene4_24.png", "scene3_10.png", "scene3_21.png", "scene3_38.png" })
						},
						new GameDecorationViewModel
						{
							Name = "pravaja tumba", Size = new Size(213, 290), Coordinate = new Point(1936, 771),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene2_13.png", Level = "FirstDecorationsCanvas",
							DependsOn = new ObservableCollection<string>(new[]
								{ "scene4_13.png", "scene4_20.png", "scene4_25.png", "scene3_13.png", "scene3_35.png", "scene3_18.png" })
						},
						new GameDecorationViewModel
						{
							Name = "scene4_03.png", SpecialAnimation = "MoveDown", SpecialSound = "squeek2", Size = new Size(159, 558),
							Coordinate = new Point(1524, 393), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_03.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_04.png", SpecialAnimation = "MoveDown", SpecialSound = "squeek2", Size = new Size(159, 558),
							Coordinate = new Point(1683, 393), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_04.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_07.png", Size = new Size(274, 170), Coordinate = new Point(992, 706),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_07.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_10.png", Size = new Size(294, 148), Coordinate = new Point(668, 727),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_10.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_13.png", Size = new Size(69, 48), Coordinate = new Point(2015, 728),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_13.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_17.png", SpecialAnimation = "MoveDown", SpecialSound = "squeek2", Size = new Size(140, 76),
							Coordinate = new Point(108, 789), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_17.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_20.png", SpecialAnimation = "MoveDown", SpecialSound = "creak", Size = new Size(139, 75),
							Coordinate = new Point(1998, 789), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_20.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_24.png", SpecialAnimation = "MoveDown", SpecialSound = "creak", Size = new Size(140, 154),
							Coordinate = new Point(108, 870), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_24.png"
						},
						new GameDecorationViewModel
						{
							Name = "scene4_25.png", SpecialAnimation = "MoveDown", SpecialSound = "squeek2", Size = new Size(139, 154),
							Coordinate = new Point(1998, 870), BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene4_25.png"
						},
						new GameDecorationViewModel
						{
							Name = "elka", Size = new Size(683, 1155), Coordinate = new Point(1706, 145),
							BaseImage = @"Assets\\GameEngine\\Decorations\\scene4\\scene5_03.png"
						},
					}),
				},

				#endregion

				#region level Barbie room

				new GameLevelModel
				{
					BgImgSize = new Size(2900, 1113),
					StartPoint = new Point(800, 200),
					Name = "Barbie house",
					LevelPickerImage = @"Assets\ChooseLevel\Barbie.jpg",
					IsAvailableToPlay = true,
					PresentsToShow = 8,
					PresentIcon = @"Assets\GameEngine\Presents\gift-icon.png",
					TimerPresentIcon = @"Assets\GameEngine\Presents\s2-present-2.png",
					GrayPresentIcon = @"Assets\GameEngine\Presents\gift-icon-gray.png",
					BackgroundImage = @"Assets\GameEngine\scene2_1.jpg",
					LaterBackgroundImage = @"Assets\GameEngine\scene2_2.jpg",
					PlaybackTime = TimeSpan.FromSeconds(25),
					Presents = new ObservableCollection<GamePresentViewModel>(gamePresentViewModelsForBarbie),
					Decorations = new ObservableCollection<GameDecorationViewModel>([
						new GameDecorationViewModel
						{
							Name = "WardrobeBack.png",
							DependsOn = new ObservableCollection<string>(new[]
							{
								"cp01", "cp02", "cp03", "cp04", "cp05", "cp06", "cp07", "cp08", "cp09", "cp10",
								"SkafGift01", "SkafGift02", "SkafGift03", "SkafGift04", "SkafGift05", "SkafGift06", "SkafGift07", "SkafGift08",
								"SkafGift09",
								"SkafGift11", "SkafGift12", "SkafGift13", "SkafGift14", "SkafGift15"
							}),
							SpecialSound = "creak",
							Level = "FirstDecorationsCanvas",
							Size = new Size(566, 460),
							Coordinate = new Point(1619, 215),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie1_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp01",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Size = new Size(179, 50),
							Coordinate = new Point(1628, 552),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf1_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp02",
							SpecialSound = "squeek",
							SpecialAnimation = "MoveDown",
							Size = new Size(189, 54),
							Coordinate = new Point(1976, 552),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf1_05.png",
						},


						new GameDecorationViewModel
						{
							Name = "cp03",
							SpecialSound = "squeek2",
							SpecialAnimation = "MoveDown",
							Size = new Size(99, 308),
							Coordinate = new Point(1627, 247),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf2_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp04",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Size = new Size(109, 399),
							Coordinate = new Point(1796, 247),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf2_05.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp05",
							SpecialSound = "squeek2",
							SpecialAnimation = "MoveDown",
							Size = new Size(113, 312),
							Coordinate = new Point(1976, 247),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf2_07.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp06",
							SpecialSound = "squeek",
							SpecialAnimation = "MoveDown",
							Size = new Size(182, 53),
							Coordinate = new Point(1976, 599),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf2_11.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp07",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Size = new Size(105, 310),
							Coordinate = new Point(1713, 245),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf3_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp08",
							SpecialSound = "squeek",
							SpecialAnimation = "MoveDown",
							Size = new Size(111, 405),
							Coordinate = new Point(1888, 245),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf3_05.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp09",
							SpecialSound = "squeek2",
							SpecialAnimation = "MoveDown",
							Size = new Size(113, 314),
							Coordinate = new Point(2069, 245),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf3_07.png",
						},

						new GameDecorationViewModel
						{
							Name = "cp10",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Size = new Size(191, 60),
							Coordinate = new Point(1619, 590),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie-skaf3_12.png",
						},

						new GameDecorationViewModel
						{
							Name = "Bed",
							SpecialSound = "door",
							Size = new Size(798, 345),
							Coordinate = new Point(1654, 524),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_21.png",
						},

						new GameDecorationViewModel
						{
							Name = "BedCupboard",
							SpecialSound = "chair",
							Size = new Size(266, 337),
							Level = "LaterDecorationsCanvas",
							Coordinate = new Point(2284, 599),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie7_06.png",
						},

						new GameDecorationViewModel
						{
							Name = "BookShelfEmpty",
							SpecialSound = "chair",
							DependsOn = new ObservableCollection<string>(new[] { "BookShelf", "BooksOnBookShelf" }),
							Size = new Size(392, 532),
							Level = "LaterDecorationsCanvas",
							Coordinate = new Point(2426, 567),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie3_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "BookShelf",
							DependsOn = new ObservableCollection<string>(new[] { "BooksOnBookShelf" }),
							Size = new Size(392, 532),
							SpecialAnimation = "FadeOut",
							Level = "LaterDecorationsCanvas",
							Coordinate = new Point(2426, 567),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie3b_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "BooksOnBookShelf",
							Level = "LaterDecorationsCanvas",
							Size = new Size(293, 176),
							Coordinate = new Point(2488, 485),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie3c_03.png",
						},

						new GameDecorationViewModel
						{
							Level = "FirstDecorationsCanvas",
							Name = "Carpet",
							DependsOn = new ObservableCollection<string>(new[] { "CircleTable", "PresentOnCarpet" }),
							Size = new Size(580, 175),
							Coordinate = new Point(3102, 698),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie6_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "PcChair",
							SpecialSound = "chair",
							Size = new Size(131, 200),
							Coordinate = new Point(997, 515),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_18.png",
						},

						new GameDecorationViewModel
						{
							SpecialSound = "squeek",
							Name = "CircleTable",
							Size = new Size(298, 123),
							Coordinate = new Point(1253, 665),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie7_09.png",
						},

						new GameDecorationViewModel
						{
							Name = "toy02",
							SpecialSound = "toy",
							Size = new Size(157, 119),
							Coordinate = new Point(1936, 781),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie7_12.png",
						},

						new GameDecorationViewModel
						{
							Name = "barbie8_03.png",
							Size = new Size(172, 275),
							Level = "LaterDecorationsCanvas",
							Coordinate = new Point(2432, 25),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "toy03",
							SpecialSound = "toy",
							Size = new Size(96, 102),
							Coordinate = new Point(1822, 137),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_06.png",
						},

						new GameDecorationViewModel
						{
							Name = "barbie8_09.png",
							Size = new Size(126, 233),
							Level = "LaterDecorationsCanvas",
							Coordinate = new Point(2306, 155),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_09.png",
						},

						new GameDecorationViewModel
						{
							Name = "barbie8_12.png",
							Size = new Size(160, 235),
							Coordinate = new Point(925, 264),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_12.png",
						},

						new GameDecorationViewModel
						{
							Name = "barbie8_15.png",
							Size = new Size(100, 197),
							Coordinate = new Point(2206, 292),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_15.png",
						},

						new GameDecorationViewModel
						{
							Name = "toy01.png",
							SpecialSound = "bear",
							Size = new Size(160, 176),
							Coordinate = new Point(2083, 711),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_21.png",
						},

						new GameDecorationViewModel
						{
							Name = "Lamp.png",
							Size = new Size(147, 563),
							Coordinate = new Point(559, 325),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_10.png",
						},

						new GameDecorationViewModel
						{
							Name = "Armchair1.png",
							Size = new Size(479, 314),
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(287, 730),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie8_24.png",
						},

						new GameDecorationViewModel
						{
							Name = "toy05",
							SpecialSound = "toy",
							Size = new Size(70, 122),
							Coordinate = new Point(1758, 123),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "RedCupboard",
							Size = new Size(132, 352),
							Coordinate = new Point(1180, 290),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_07.png",
						},

						new GameDecorationViewModel
						{
							Name = "Mirror",
							Size = new Size(365, 485),
							Coordinate = new Point(1273, 179),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie5_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "PcTable",
							SpecialSound = "squeek",
							DependsOn = new ObservableCollection<string>(new[] { "Pc" }),
							Size = new Size(229, 347),
							Coordinate = new Point(768, 389),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_13.png",
						},

						new GameDecorationViewModel
						{
							Name = "Pc",
							SpecialSound = "computer",
							Size = new Size(168, 148),
							Coordinate = new Point(809, 419),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie2_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "Armchair2",
							SpecialSound = "creak",
							Size = new Size(310, 223),
							Coordinate = new Point(723, 582),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie7_03.png",
						},

						new GameDecorationViewModel
						{
							Name = "photo",
							SpecialSound = "shutter",
							Size = new Size(106, 108),
							Coordinate = new Point(1064, 389),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_15.png",
						},

						new GameDecorationViewModel
						{
							Name = "barbie9_26.png",
							SpecialSound = "squeek2",
							Size = new Size(291, 152),
							Coordinate = new Point(808, 736),
							BaseImage = @"Assets\GameEngine\Decorations\scene2\barbie9_26.png",
						}


					])
				},

				#endregion

				#region level Living room

				new GameLevelModel
				{
					PresentsToShow = 8,
					BgImgSize = new Size(3000, 1113),
					Name = "Living room",
					StartPoint = new Point(1400, 0),
					LevelPickerImage = @"Assets\ChooseLevel\LivingRoom.jpg",
					IsAvailableToPlay = true,
					BackgroundImage = @"Assets\GameEngine\scene1_1.jpg",
					LaterBackgroundImage = @"Assets\GameEngine\scene1_2.jpg",
					PresentIcon = @"Assets\GameEngine\Presents\s1-present-1.png",
					GrayPresentIcon = @"Assets\GameEngine\Presents\s1-present-1-gray.png",
					PlaybackTime = TimeSpan.FromSeconds(20),
					Presents = new ObservableCollection<GamePresentViewModel>([
						new GamePresentViewModel
						{
							Name = "prezens_03.png",
							Size = new Size(50, 47),
							Coordinate = new Point(1532, 185),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_03.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_07.png",
							Size = new Size(48, 50),
							Level = "LaterPresentsCanvas",
							Coordinate = new Point(2204, 240),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_07.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_10.png",
							Size = new Size(51, 46),
							Coordinate = new Point(1373, 290),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_10.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_15.png",
							Size = new Size(56, 42),
							Coordinate = new Point(1226, 418),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_15.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_19.png",
							Size = new Size(51, 47),
							Coordinate = new Point(527, 624),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_19.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_22.png",
							Size = new Size(42, 45),
							Coordinate = new Point(654, 630),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_22.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_25.png",
							Size = new Size(48, 50),
							Coordinate = new Point(1804, 630),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_25.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_28.png",
							Size = new Size(50, 50),
							Coordinate = new Point(410, 634),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_28.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_31.png",
							Size = new Size(49, 46),
							Level = "LaterPresentsCanvas",
							Coordinate = new Point(1918, 644),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_31.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_34.png",
							Size = new Size(54, 48),
							Coordinate = new Point(869, 639),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_34.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_36.png",
							Size = new Size(55, 48),
							Coordinate = new Point(1119, 639),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_36.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_41.png",
							Size = new Size(82, 60),
							Coordinate = new Point(226, 680),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_41.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_49.png",
							Size = new Size(64, 61),
							Level = "LaterPresentsCanvas",
							Coordinate = new Point(2044, 710),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_49.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_53.png",
							Size = new Size(69, 74),
							Coordinate = new Point(1685, 740),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_53.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_57.png",
							Size = new Size(97, 96),
							Coordinate = new Point(1248, 786),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_57.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_60.png",
							Size = new Size(82, 78),
							Coordinate = new Point(1450, 786),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_60.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_62.png",
							Size = new Size(106, 84),
							Level = "LaterPresentsCanvas",
							Coordinate = new Point(2705, 805),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_62.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_67.png",
							Size = new Size(112, 104),
							Coordinate = new Point(821, 882),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_67.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_72.png",
							Size = new Size(108, 117),
							Level = "LaterPresentsCanvas",
							Coordinate = new Point(2260, 935),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_72.png",
						},

						new GamePresentViewModel
						{
							Name = "prezens_75.png",
							Size = new Size(156, 125),
							Coordinate = new Point(31, 953),
							BaseImage = @"Assets\GameEngine\Presents\scene1\prezens_75.png",
						}

					]),
					Decorations = new ObservableCollection<GameDecorationViewModel>(new List<GameDecorationViewModel>
					{
						new GameDecorationViewModel
						{
							Name = "Tree",
							Coordinate = new Point(408, 129),
							Size = new Size(236, 570),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene6_06.png"
						},
						new GameDecorationViewModel
						{
							Name = "RightWallPicture",
							SpecialSound = "squeek2",
							Coordinate = new Point(2394, 0),
							Level = "FirstLaterDecorationsCanvas",
							Size = new Size(606, 421),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_02.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairBack",
							SpecialSound = "chair",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(1729, 343),
							Size = new Size(203, 377),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene7_12.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairLeftBack",
							SpecialSound = "chair",
							Coordinate = new Point(1571, 324),
							Size = new Size(219, 432),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_04.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairRightBack",
							SpecialSound = "chair",
							SpecialAnimation = "MoveDown",
							Level = "FirstDecorationsCanvas",
							Coordinate = new Point(1984, 332),
							Size = new Size(256, 424),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_07.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairRight",
							SpecialSound = "chair",
							SpecialAnimation = "MoveDown",
							Level = "FirstLaterDecorationsCanvas",
							Coordinate = new Point(2140, 305),
							Size = new Size(366, 577),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene2_06.png"
						},
						new GameDecorationViewModel
						{
							Name = "Table",
							SpecialSound = "door",
							Coordinate = new Point(1698, 342),
							Size = new Size(668, 458),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene5_03.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairLeft",
							SpecialSound = "chair",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(1613, 293),
							Size = new Size(296, 561),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene2_03.png"
						},
						new GameDecorationViewModel
						{
							Name = "Totem1",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(619, 495),
							Size = new Size(98, 196),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_11.png"
						},
						new GameDecorationViewModel
						{
							Name = "Totem2",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(378, 515),
							Size = new Size(88, 185),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_14.png"
						},
						new GameDecorationViewModel
						{
							Name = "Totem3",
							SpecialSound = "creak",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(506, 532),
							Size = new Size(89, 182),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene4_17.png"
						},
						new GameDecorationViewModel
						{
							Name = "ArmchairBack",
							SpecialSound = "squeek2",
							Coordinate = new Point(697, 497),
							Size = new Size(332, 306),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene2_12.png"
						},
						new GameDecorationViewModel
						{
							Name = "JournalTable",
							SpecialSound = "squeek",
							Coordinate = new Point(97, 417),
							Size = new Size(499, 437),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene2_09.png"
						},

						new GameDecorationViewModel
						{
							Name = "Picture1",
							Coordinate = new Point(1478, 120),
							Size = new Size(158, 150),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene6_03.png"
						},
						new GameDecorationViewModel
						{
							Name = "Picture2",
							Coordinate = new Point(1322, 249),
							Size = new Size(156, 151),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene6_09.png"
						},
						new GameDecorationViewModel
						{
							Name = "Picture3",
							Coordinate = new Point(1146, 369),
							Size = new Size(158, 157),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene6_15.png"
						},
						new GameDecorationViewModel
						{
							Name = "ArmchairFront",
							SpecialSound = "creak",
							Coordinate = new Point(193, 502),
							Size = new Size(625, 458),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene1_10.png"
						},
						new GameDecorationViewModel
						{
							Name = "TrashBin",
							Coordinate = new Point(2695, 616),
							Size = new Size(268, 320),
							Level = "LaterDecorationsCanvas",
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene1_13.png"
						},
						new GameDecorationViewModel
						{
							Name = "Robot",
							SpecialSound = "computer",
							Coordinate = new Point(1234, 665),
							Size = new Size(191, 336),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene1_16.png"
						},
						new GameDecorationViewModel
						{
							Name = "ChairFront",
							SpecialSound = "chair",
							SpecialAnimation = "MoveDown",
							Coordinate = new Point(1929, 285),
							Size = new Size(412, 654),
							BaseImage = @"Assets\GameEngine\Decorations\scene1\scene1_03.png"
						},

					})
				},

				#endregion

			}.ToList();
		}
	}
}
