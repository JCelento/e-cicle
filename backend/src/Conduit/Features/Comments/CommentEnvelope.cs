using EletronicPartsCatalog.Domain;

namespace EletronicPartsCatalog.Features.Comments
{
    public class CommentEnvelope
    {
        public CommentEnvelope(Comment comment)
        {
            Comment = comment;
        }

        public Comment Comment { get; }
    }
}