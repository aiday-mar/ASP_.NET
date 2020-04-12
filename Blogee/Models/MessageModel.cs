using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogee.Models
{
    public static class MessageModel
    {
        // we initiate the number of messages to zero initially.
        public static int numberMessage { get; set; } = 0;

        public static bool update { get; set; } = false;

        public static List<IndividualMessageModel> listMessages;

    }
}