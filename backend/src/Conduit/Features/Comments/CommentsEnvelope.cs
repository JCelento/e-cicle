using System.Collections.Generic;
using EletronicPartsCatalog.Domain;

namespace EletronicPartsCatalog.Features.Comments
{
    public class CommentsEnvelope
    {
        public CommentsEnvelope(List<Comment> comments)
        {
            Comments = comments;
        }

        public List<Comment> Comments { get; }
    }
}