using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pumpkin.AI.BehaviorTree
{
    public interface ISavable
    {
        string Guid { get; }
        void Save(BehaviorTreeDesignContainer designContainer);
    }
}
