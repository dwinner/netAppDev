using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Api.Queries;

public class QueryHandler(IPostRepository postRepository) : IQueryHandler
{
   public async Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query) => await postRepository.ListAllAsync();

   public async Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query)
   {
      var post = await postRepository.GetByIdAsync(query.Id);
      return [post];
   }

   public async Task<List<PostEntity>> HandleAsync(FindPostsByAuthorQuery query) =>
      await postRepository.ListByAuthorAsync(query.Author);

   public async Task<List<PostEntity>> HandleAsync(FindPostsWithCommentsQuery query) =>
      await postRepository.ListWithCommentsAsync();

   public async Task<List<PostEntity>> HandleAsync(FindPostsWithLikesQuery query) =>
      await postRepository.ListWithLikesAsync(query.NumberOfLikes);
}