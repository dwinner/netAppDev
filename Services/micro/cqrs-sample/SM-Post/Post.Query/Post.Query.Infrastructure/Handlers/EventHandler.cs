using Post.Common.Events;
using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Infrastructure.Handlers;

public class EventHandler(IPostRepository postRepository, ICommentRepository commentRepository)
   : IEventHandler
{
   public async Task On(PostCreatedEvent anEvent)
   {
      var post = new PostEntity
      {
         PostId = anEvent.Id,
         Author = anEvent.Author,
         DatePosted = anEvent.DatePosted,
         Message = anEvent.Message
      };

      await postRepository.CreateAsync(post);
   }

   public async Task On(MessageUpdatedEvent anEvent)
   {
      var post = await postRepository.GetByIdAsync(anEvent.Id);
      if (post == null)
      {
         return;
      }

      post.Message = anEvent.Message;
      await postRepository.UpdateAsync(post);
   }

   public async Task On(PostLikedEvent anEvent)
   {
      var post = await postRepository.GetByIdAsync(anEvent.Id);
      if (post == null)
      {
         return;
      }

      post.Likes++;
      await postRepository.UpdateAsync(post);
   }

   public async Task On(CommentAddedEvent anEvent)
   {
      var comment = new CommentEntity
      {
         PostId = anEvent.Id,
         CommentId = anEvent.CommentId,
         CommentDate = anEvent.CommentDate,
         Comment = anEvent.Comment,
         Username = anEvent.Username,
         Edited = false
      };

      await commentRepository.CreateAsync(comment);
   }

   public async Task On(CommentUpdatedEvent anEvent)
   {
      var comment = await commentRepository.GetByIdAsync(anEvent.CommentId);
      if (comment == null)
      {
         return;
      }

      comment.Comment = anEvent.Comment;
      comment.Edited = true;
      comment.CommentDate = anEvent.EditDate;

      await commentRepository.UpdateAsync(comment);
   }

   public async Task On(CommentRemovedEvent anEvent)
   {
      await commentRepository.DeleteAsync(anEvent.CommentId);
   }

   public async Task On(PostRemovedEvent anEvent)
   {
      await postRepository.DeleteAsync(anEvent.Id);
   }
}