using DataAccesLayer;
using Xunit;

namespace XUnitTestProject
{
    public class DataLayerTests
    {
        [Fact]
        public void GetUser_ValidId_ReturnsUsersWithPosts()
        {
            var ds = new DataService();
            var user = ds.GetUser(1);
            Assert.Equal(2, user.Posts.Count);
        }

        [Fact]
        public void GetPost_ValidId_ReturnsFullPost()
        {
            var ds = new DataService();
            var post = ds.GetPost(83073);
            Assert.Equal(post.Score, 665);
            Assert.Equal(5, post.PostTags.Count);
            Assert.Equal(13, post.Comments.Count);
        }

        [Fact]
        public void GetQuestion_ValidId_ReturnsFullQuestion()
        {
            var ds = new DataService();
            var question = ds.GetQuestion(19);
            Assert.True(question.GetPost().Title.Contains("the fastest way to get the value of"));
            Assert.Equal(531, question.AcceptedAnswerId);
        }

        [Fact]
        public void GetAnswer_ValidId_ReturnsFullAnswer()
        {
            var ds = new DataService();
            var question = ds.GetAnswer(71);
            Assert.True(question.GetPost().Body.Contains("general description of a technique"));
            Assert.Equal(19, question.Parentid);
        }

        [Fact]
        public void GetPosts_ValidID_ReturnsPostWithTags() 
        {
            var ds = new DataService();
            var post = ds.GetPosts_Tags(19);
            Assert.Equal(5, post.PostTags.Count);
        }

        [Fact]
        public void InsertMarking_ValidID_InsertsMarking()
        {
            var ds = new DataService();
            var ret = ds.AddMarking(1, 2, "testing");
            Assert.Equal(1, ret);
        }

        /*
         [Fact]
         public void DBProcedure_SearchQuestionByID_ReturnsQuestionList()
         {
             var ds = new DataService();
            // var results = ds.SeachQuestionsByTag("YourMum", 10);
            // Assert.NotEmpty(results);
         }
         */
        //This test Shows the procedure is returning records, how ever needs further care
        [Fact]
         public void Searching_using_type_Strings()
         {
             var ds = new DataService();
             var text = ds.Searching_usingtype_String();
             Assert.Equal(true, text);

         }
         [Fact]
         public void Retrieve_Answers_Procedure()
         {
             var ds = new DataService();
             var text = ds.Retrieve_Answers_Procedure("java constructor");
             Assert.NotEqual(0, text.Count);

         }
        [Fact]
        public void AddPosttoHistory_Test()
        {
            var ds = new DataService();
           ds.AddQuestionToHistory(19, 13);
            Assert.True(ds.GetHistory().Contains(ds.GetHistoryItem(19)));
        }
        //Assert.NotEmpty(ds.GetHistory()); 
        /*[Fact]
        public void SearchQuestionsByTag_Tag_ReturnsFullQuestionsByTag()
        {
            var ds = new DataService();
            var questions = ds.SearchQuestionsByTag("java",10);
            Assert.Equal(34571, questions[0].PostId1);
        }*/

    }
}
