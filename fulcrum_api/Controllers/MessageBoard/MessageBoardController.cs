using fulcrum_api.Attributes.Controller;
using fulcrum_api.Constants;
using fulcrum_api.Controllers.FulcrumBase;
using fulcrum_api.FormObjects.MessageBoard;
using fulcrum_services.Models.MessageBoard;
using fulcrum_services.Models.SessionManagement;
using fulcrum_services.NHibernate.Criteria;
using fulcrum_services.NHibernate.CustomTypes;
using fulcrum_services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace fulcrum_api.Controllers.MessageBoard
{
    [FulcrumRoutePrefix("/messages")]
    public class MessageBoardController : FulcrumBaseController
    {
        private readonly IGenericService _genericService;

        public MessageBoardController(IGenericService genericService)
        {
            _genericService = genericService;
        }

        [FulcrumRoute("/save", true, F.POST)]
        public HttpResponseMessage addMessages(HttpRequestMessage request, MessageBoardFO fo)
        {
            _genericService.saveOrUpdate(fo.topic);
            _genericService.saveOrUpdate<Message>(fo.topic.messages);

            IList<Comment> comments = new List<Comment>();
            foreach (var msg in fo.topic.messages)
            {
                if (msg.comments != null && msg.comments.Count > 0)
                {
                    foreach (var c in msg.comments)
                    {
                        comments.Add(c);
                    }
                }
            }
            _genericService.saveOrUpdate<Comment>(comments);

            return request.CreateResponse(HttpStatusCode.OK, "Message successfully created");
        }

        [FulcrumRoute("/fromDate/{earliest:datetime}", true, F.GET)]
        public IList<Topic> getTopicsFromEarliestDate(HttpRequestMessage request, DateTime earliest)
        {
            string currentCompany = LoggedUser.getCompany();

            restrictions.append("company", Company.getCompany(currentCompany));
            restrictions.append("createdTime", earliest, FQualifier.GE);

            return _genericService.get<Topic>(restrictions);
        }

    }
}