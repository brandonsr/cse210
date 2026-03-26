class Program
{
    static void Main(string[] args)
    {
        // Create videos
        List<Video> videos = new List<Video>();

        Video video1 = new Video("How to Learn C# Fast", "TechTutor", 620);
        video1.AddComment(new Comment("Alice", "This was super helpful, thank you!"));
        video1.AddComment(new Comment("Bob", "Best C# tutorial I have seen so far."));
        video1.AddComment(new Comment("Carlos", "I finally understand classes now."));

        Video video2 = new Video("Top 10 VS Code Tips", "CodePro", 485);
        video2.AddComment(new Comment("Diana", "Tip #7 changed my life."));
        video2.AddComment(new Comment("Ethan", "I use VS Code daily and still learned something new!"));
        video2.AddComment(new Comment("Fiona", "Please make a follow-up video."));
        video2.AddComment(new Comment("George", "Great content as always."));

        Video video3 = new Video("Git and GitHub for Beginners", "DevMentor", 910);
        video3.AddComment(new Comment("Hannah", "Finally a clear explanation of branching."));
        video3.AddComment(new Comment("Ivan", "This helped me stop fearing the command line."));
        video3.AddComment(new Comment("Julia", "I had to watch this twice, so good."));

        Video video4 = new Video("Object-Oriented Programming Explained", "LearnWithMe", 774);
        video4.AddComment(new Comment("Kevin", "Abstraction makes so much sense now."));
        video4.AddComment(new Comment("Laura", "Encapsulation and inheritance covered perfectly."));
        video4.AddComment(new Comment("Mike", "I wish my professor explained it this clearly."));
        video4.AddComment(new Comment("Nina", "Subscribed immediately after this video."));

        videos.Add(video1);
        videos.Add(video2);
        videos.Add(video3);
        videos.Add(video4);

        // Display all videos and their comments
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title:    {video.Title}");
            Console.WriteLine($"Author:   {video.Author}");
            Console.WriteLine($"Length:   {video.LengthInSeconds} seconds");
            Console.WriteLine($"Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("--- Comments ---");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"  {comment.Name}: {comment.Text}");
            }

            Console.WriteLine(); // blank line between videos
        }
    }
}
