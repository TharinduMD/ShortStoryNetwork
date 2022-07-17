using System;
using System.Collections.Generic;
using System.Text;

namespace ShortStoryNetwork.Repository.Interfaces
{
    public interface IStatVowelRepository
    {
        string Message { get; set; }
        string Result { get; set; }
        bool AddStatVowel(string post);
        bool UpdateStateVowel(string post);

    }
}
