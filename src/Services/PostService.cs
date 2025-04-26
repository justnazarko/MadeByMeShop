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
                .ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Seller)
                .Include(p => p.CommentsList)
                .FirstOrDefault(p => p.Id == id);
        }

        public Post CreatePost(CreatePostDto createPostDto)
        {
            var post = new Post
            {
                Title = createPostDto.Title,
                Description = createPostDto.Description,
                Price = createPostDto.Price,
                PhotoLink = createPostDto.PhotoLink,
                CategoryId = createPostDto.CategoryId,
                SellerId = createPostDto.SellerId
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
                post.Price = updatePostDto.Price ?? post.Price;  // Додано оператор ?? для decimal?
                post.PhotoLink = updatePostDto.PhotoLink ?? post.PhotoLink;
                post.CategoryId = updatePostDto.CategoryId ?? post.CategoryId;  // Додано оператор ?? для int?

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
    }
}