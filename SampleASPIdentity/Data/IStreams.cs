using SampleASPIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleASPIdentity.Data
{
    public interface IStreams : ICrud<stream>
    {
        Task UpdateWithID(int id, stream obj);
    }
}
