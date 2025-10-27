using flip_flop_core.Provider;
using flip_flop_dal.Workers;
using NUnit.Framework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using flip_flop_dal.Enitites;

namespace flip_flop_tests.Tests
{
    [TestFixture(Category = "Data Access Layer")]
    public class DataAccessLayerTests
    {
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = SingletonProvider.ServiceProvider.GetService<IUnitOfWork>();
        }

        [OneTimeTearDown]
        public void RemoveData()
        {
            var players=_unitOfWork.PlayerRepository.FetchAll();

            foreach (var player in players)
            {
                _unitOfWork.PlayerRepository.Delete(player);
            }

            _unitOfWork.Save();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void Data_Access_Layer_Add_Player(int playerId)
        {
            try
            {
                _unitOfWork.PlayerRepository.Add(new Player
                {
                    PlayerId = playerId,
                    ExternalId = 12,
                    PartnerId = 1,
                    Username = "Test"
                });

                _unitOfWork.Save();

                var player = _unitOfWork.PlayerRepository.FetchByPrimaryKey(playerId);

                Assert.That(player != null);
            }
            catch
            {
                Assert.Fail();
            }

        }

        //[TestCase(4)]
        //[TestCase(5)]
        //[TestCase(6)]
        //public void Data_Access_Layer_Update_Player(int playerId)
        //{
        //    try
        //    {
        //        _unitOfWork.PlayerRepository.Add(new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test"
        //        });

        //        _unitOfWork.PlayerRepository.Update(new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test Test"
        //        });

        //        _unitOfWork.Save();

        //        var player = _unitOfWork.PlayerRepository.FetchByPrimaryKey(playerId);


        //        Assert.That(player.Username == "Test Test");
        //    }
        //    catch (Exception ex)
        //    {
        //        Assert.Fail();
        //    }
        //}


        //[TestCase(7)]
        //[TestCase(8)]
        //[TestCase(9)]
        //public void Data_Access_Layer_Delete_Player(int playerId)
        //{
        //    try
        //    {
        //        _unitOfWork.PlayerRepository.Add(new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test"
        //        });

        //        _unitOfWork.Save();

        //        _unitOfWork.PlayerRepository.Delete(new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test"
        //        });

        //        _unitOfWork.Save();

        //        var player = _unitOfWork.PlayerRepository.FetchByPrimaryKey(playerId);

        //        Assert.That(player == null);
        //    }
        //    catch
        //    {
        //        Assert.Fail();
        //    }
        //}

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public void Data_Access_Layer_Find_Where_Player(int playerId)
        {
            try
            {
                _unitOfWork.PlayerRepository.Add(new Player
                {
                    PlayerId = playerId,
                    ExternalId = 12,
                    PartnerId = 1,
                    Username = "Test"
                });

                _unitOfWork.Save();

                var player = _unitOfWork.PlayerRepository.FindWhere((pl)=>pl.PlayerId==playerId);

                Assert.That(player != null);
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public void Data_Access_Layer_Add_Range_Player(int playerId)
        {
            try
            {
                _unitOfWork.PlayerRepository.Add(new List<Player>{new Player
                {
                    PlayerId = playerId,
                    ExternalId = 12,
                    PartnerId = 1,
                    Username = "Test"
                }});

                _unitOfWork.Save();
            }
            catch
            {
                Assert.Fail();
            }
        }

        //[TestCase(16)]
        //[TestCase(17)]
        //[TestCase(18)]
        //public void Data_Access_Layer_Delete_Range_Player(int playerId)
        //{
        //    try
        //    {
        //        _unitOfWork.PlayerRepository.Add(new List<Player>{new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test"
        //        }});

        //        _unitOfWork.Save();

        //        _unitOfWork.PlayerRepository.Delete(new List<Player>{new Player
        //        {
        //            PlayerId = playerId,
        //            ExternalId = 12,
        //            PartnerId = 1,
        //            Username = "Test"
        //        }});

        //        _unitOfWork.Save();
        //    }
        //    catch
        //    {
        //        Assert.Fail();
        //    }
        //}


        [TestCase]
        [TestCase]
        [TestCase]
        public void Data_Access_Layer_Fetch_All_Player()
        {
            try
            {
                var players = _unitOfWork.PlayerRepository.FetchAll();

                Assert.That(players != null);
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
