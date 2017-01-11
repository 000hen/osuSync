﻿using Sync.MessageFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefaultPlugin.Filters
{
    class DefaultFormat : FilterBase, IDanmaku, IOsu
    {
        public override void onMsg(ref MessageBase msg)
        {
            msg.user.setPerfix("<");
            msg.user.setSuffix(">: ");
        }
    }
}