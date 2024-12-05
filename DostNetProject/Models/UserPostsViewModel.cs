namespace DostNetProject.Models
{
    public class UserPostsViewModel
    {
        public DostNetUser User { get; set; }
        public List<Post> Posts { get; set; }
        public List<Article> Articles { get; set; }
        public List<InterViewQuestion> InterViewQuestions { get; set; }
    }
}
