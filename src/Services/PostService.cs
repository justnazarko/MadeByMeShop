using MadeByMe.src.DTOs;
using MadeByMe.src.Models;
using Microsoft.EntityFrameworkCore;
using System;

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
            return _context.Posts.ToList();
            //return _context.Posts.Include(p => p.Comments).ToList();
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
            //return _context.Posts.Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
        }

        public void CreatePost(CreatePostDto createPostDto)
        {
            var post = new Post 
            {
                Title = createPostDto.Title,
                Description = createPostDto.Description,
                Price = createPostDto.Price,
                PhotoLink = createPostDto.PhotoLink,
                Status = createPostDto.Status
            };


            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public Post UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                post.Title = updatePostDto.Title;
                post.Description = updatePostDto.Description;
                post.Price = updatePostDto.Price;
                post.PhotoLink = updatePostDto.PhotoLink;
                post.Status = updatePostDto.Status;
            }

            _context.SaveChanges();

            return post;
        }

        public bool RemovePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
