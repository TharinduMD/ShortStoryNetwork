using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShortStoryNetwork.Core;
using ShortStoryNetwork.Data;
using ShortStoryNetwork.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShortStoryNetwork.Repository
{
    public class StatVowelRepository : IStatVowelRepository
    {
        private readonly ILogger<StatVowelRepository> _logger;
        private readonly Context _context;

        public string Message { get; set; }
        public string Result { get; set; }
        public StatVowelRepository(ILogger<StatVowelRepository> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        //POST
        public bool AddStatVowel(string post)
        {
            var success = false;
            try
            {
                var singleVowelCount = CalculateSingleVowels(post);
                var pairVowelCount = CalculatePairOfVowels(post);
                var totalWordCount = CalculateTotalWordCount(post);

                var stateVowel = new StatVowel
                {
                    Id = "VS02",
                    Date = DateTime.Today,
                    SingleVowelCount = singleVowelCount,
                    PairVowelCount = pairVowelCount,
                    TotalWordCount = totalWordCount
                };
                _context.StatVowels.Add(stateVowel);
                _context.SaveChanges();
                success = true;
                Result = stateVowel.Id;
                return success;

            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while saving";
                throw;
            }
        }

        public bool UpdateStateVowel(string post)
        {
            var success = false;
            try
            {
                var vowelState = _context.StatVowels.FirstOrDefault(m => m.Date == DateTime.Today);
                if (vowelState != null)
                {
                    var singleVowelCount = CalculateSingleVowels(post);
                    var pairVowelCount = CalculatePairOfVowels(post);
                    var totalWordCount = CalculateTotalWordCount(post);

                    vowelState.SingleVowelCount += singleVowelCount;
                    vowelState.PairVowelCount += pairVowelCount;
                    vowelState.TotalWordCount += totalWordCount;

                    _context.StatVowels.Update(vowelState);
                    _context.SaveChanges();
                    success = true;
                    Result = vowelState.Id;
                }
                return success;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while saving";
                throw;
            }
        }

        public StatVowel GetStateVowelByDay(DateTime date)
        {
            StatVowel statVowel = null;
            try
            {
                statVowel = _context.StatVowels.FirstOrDefault(x => x.Date.Date == date.Date);
                return statVowel;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while retriving";
                throw;
            }
        }

        public List<UserInfo> GetUserInfoList(string searchField, string searchText)
        {
            var userInfo = new List<UserInfo>();
            try
            {
                var sql = string.Empty;
                if (!string.IsNullOrWhiteSpace(searchField))
                    sql = $"Select * from dbo.UserInfo where {searchField} like '%{searchText}%' order by {searchField}";
                else
                    sql = "Select * from dbo.UserInfo";
                userInfo = _context.UserInfos.FromSqlRaw(sql).ToList();
            }
            catch (Exception e)
            {
                Message = "Error occured";
                _logger.LogError("Error occured when retriving " + e);
            }
            return userInfo;
        }

        // Calculate single vowel in the word
        public int CalculateSingleVowels(string post)
        {
            int count = 0;
            List<char> vowels = new List<char> { 'A', 'E', 'I', 'O', 'U' };
            try
            {
                if (!string.IsNullOrEmpty(post))
                {
                    string[] wordList = post.Split(' ');
                    foreach (var word in wordList)
                    {
                        for (int i = 0; i + 1 < word.Length; i++)
                        {
                            if (vowels.Contains(char.ToUpper(word[i])) && !vowels.Contains(char.ToUpper(word[i + 1])))
                            {
                                count++;
                            }
                        }
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while calculating";
                throw;
            }
        }

        // Calculate pair of vowels in the word
        public int CalculatePairOfVowels(string post)
        {
            int count = 0;
            List<char> vowels = new List<char> { 'A', 'E', 'I', 'O', 'U' };
            try
            {
                if (!string.IsNullOrEmpty(post))
                {
                    string[] wordList = post.Split(' ');
                    foreach (var word in wordList)
                    {
                        for (int i = 0; i + 1 < word.Length; i++)
                        {
                            if (vowels.Contains(char.ToUpper(word[i])) && vowels.Contains(char.ToUpper(word[i + 1])))
                            {
                                count++;
                            }
                        }
                    }
                }
                return count;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while calculating";
                throw;
            }
        }

        public int CalculateTotalWordCount(string post)
        {
            int count = 0;
            try
            {
                if (!string.IsNullOrEmpty(post))
                {
                    string[] wordList = post.Split(' ');
                    count = wordList.Length;
                }

                return count;
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                Message = "Error occured while calculating";
                throw;
            }
        }

    }
}
