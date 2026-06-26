using Post.Common.Events;

namespace Post.Query.Infrastructure.Handlers;

public interface IEventHandler
{
   Task On(PostCreatedEvent anEvent);
   Task On(MessageUpdatedEvent anEvent);
   Task On(PostLikedEvent anEvent);
   Task On(CommentAddedEvent anEvent);
   Task On(CommentUpdatedEvent anEvent);
   Task On(CommentRemovedEvent anEvent);
   Task On(PostRemovedEvent anEvent);
}