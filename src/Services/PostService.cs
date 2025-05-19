using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MadeByMe.src.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.Photos)
                .ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.CommentsList)
                .Include(p => p.Photos)
                .FirstOrDefault(p => p.Id == id);
        }

        public Post CreatePost(CreatePostDto createPostDto, string sellerId)
        {
            var post = new Post
            {
                Title = createPostDto.Title,
                Description = createPostDto.Description,
                Price = createPostDto.Price,
                CategoryId = createPostDto.CategoryId,
                SellerId = sellerId
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
            return post;
        }

        public Post UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                post.Title = updatePostDto.Title ?? post.Title;
                post.Description = updatePostDto.Description ?? post.Description;
                post.Price = updatePostDto.Price ?? post.Price;
                post.CategoryId = updatePostDto.CategoryId ?? post.CategoryId;

                _context.SaveChanges();
            }
            return post;
        }

        public bool DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Post> SearchPosts(string searchTerm)
        {
            var query = _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.Photos)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Title.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            return query.ToList();
        }
    }
}