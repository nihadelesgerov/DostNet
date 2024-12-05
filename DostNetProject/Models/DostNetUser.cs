using Microsoft.AspNetCore.Identity;

namespace DostNetProject.Models
{
    public class DostNetUser : IdentityUser
    {
        // Relation to Posts
        public ICollection<Post>? Posts { get; set; } 

        // realtion to articles 
        public ICollection<Article>? Articles { get; set; }

        // relation to interviewQuestions
        public ICollection<InterViewQuestion>? InterViewQuestions { get; set; }
    }
}
