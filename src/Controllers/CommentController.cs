﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;
using MadeByMe.src.Models;
using MadeByMe.src.DTOs;
using MadeByMe.src.Services;
using Microsoft.AspNetCore.Identity;

public class CommentController : Controller
{
	private readonly ApplicationDbContext _context;
	private readonly CommentService _commentService;
    private readonly UserManager<ApplicationUser> _userManager;

    public CommentController(ApplicationDbContext context, CommentService commentService, UserManager<ApplicationUser> userManager = null)
    {
        _context = context;
        _commentService = commentService;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
	{
		var comments = await _context.Comments.ToListAsync();
		return View(comments);
	}

	public async Task<IActionResult> Details(int id) // ?
	{
		var comment = await _context.Comments.FindAsync(id);
		if (comment == null) return NotFound();

		return View(comment);
	}

	//public IActionResult Create(int postId)
	//{
	//	return View(new Comment { PostId = postId });
	//}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(CreateCommentDto dto)
	{
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }

        if (!ModelState.IsValid)
		{
            return View(dto);
        }

        var userId = _userManager.GetUserId(User);

        var comment = _commentService.AddComment(dto, userId);

		return RedirectToAction("Details", "Post", new { id = comment.PostId });
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	[Authorize]
	public IActionResult Delete(int id)
	{
        var comment = _commentService.GetCommentById(id);
        if (comment == null)
            return NotFound();

        
        if (User.IsInRole("Admin"))
        {
            _commentService.DeleteComment(id);
            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        var currentUserName = User.Identity.Name;
        if (comment.User.UserName == currentUserName)
        {
            _commentService.DeleteComment(id);
            return RedirectToAction("Details", "Post", new { id = comment.PostId });
        }

        return Forbid();

		//if (!_commentService.DeleteComment(id))
		//{
		//	return NotFound();
		//}
		//else
		//{
		//	return RedirectToAction("Index");
		//}

	}
}
