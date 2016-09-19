using Octokit;
using Octokit.Helpers;
using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;


namespace GitRlease
{
    class Program
    {
        static void Main(string[] args)
        {
            myAsyncMethod();

            Console.ReadLine();

            //string versionNumber = args[0];
            //Console.WriteLine(args.Count());
            //Console.WriteLine(args[0]);

            //var newRelease = new ReleaseUpdate(versionNumber);
            //newRelease.Name = "Version One Point Oh";
            //newRelease.Body = "**This** is some *Markdown*";
            //newRelease.Draft = true;
            //newRelease.Prerelease = false;

            //var result = client.Release.Create("octokit", "octokit.net", newRelease);
            //Console.WriteLine("Created release id {0}", release.Id);
        }

        public static async void myAsyncMethod()
        {
            var client = new GitHubClient(new ProductHeaderValue("JennysAwesomeGitRelease"));

            var tokenAuth = new Credentials("");
            client.Credentials = tokenAuth;

            //var db = new GitDatabaseClient();
            //db.Tag.Get
            Repository result = await client.Repository.Get("Jennyf19", "BinaryTree");
            Console.WriteLine(result.Id);
            Console.WriteLine(result.GitUrl);
            Console.WriteLine(result.FullName);
            Console.WriteLine(result.StargazersCount);
            Console.WriteLine(result.Url);

            var lastReleases = await client.Repository.Release.GetAll(result.Id);
            var latest = lastReleases[0];
            Console.WriteLine("The latest release is tagged at {0} and is named {1}", latest.TagName, latest.Name);

            var newRelease = new NewRelease("BinaryTree");
            newRelease.Name = "Big Cat Adventure";
            newRelease.Body = "this is the markdown";
            newRelease.Draft = true;
            newRelease.Prerelease = false;

            var newReleaseResult = await client.Repository.Release.Create(result.Id, newRelease);
            Console.WriteLine("Created release id {0}", newRelease.TagName);

            #region Add code here to create a new tag...
            //# Working with the Git Database

            //### Tag a Commit

            //            Tags can be created through the GitHub API

            //```
            //var tag = new NewTag
            //{
            //    Message = "Tagging a new release of Octokit",
            //    Tag = "v1.0.0",
            //    Object = "ee062e0", // short SHA
            //    Type = TaggedType.Commit, // TODO: what are the defaults when nothing specified?
            //    Tagger = new Signature
            //    {
            //        Name = "Brendan Forster",
            //        Email = "brendan@github.com",
            //        Date = DateTime.UtcNow
            //    }
            //};
            //            var result = await client.GitDatabase.Tags.Create("octokit", "octokit.net", tag);
            //            Console.WriteLine("Created a tag for {0} at {1}", result.Tag, result.Sha);
            //```

            //Or you can fetch an existing tag from the API:

            //var tag = await client.GitDatabase.Tags.Get("octokit", "octokit.net", "v1.0.0");
            #endregion
            //var tag = await client.Git.Tag.Get("jennyf19", "binaryTree", "v1.0.0");
            //var tag = await client.GitDatabase.Tags.Get("jennyf19", "binaryTree", "v1.0.0");
            var tagsResult = await client.Repository.GetAllTags(result.Id);
            var tag = tagsResult.FirstOrDefault();

            //if (tag == null) Console.WriteLine("null!");
            //else
            //{
            //NewRelease data = new NewRelease(tag.Name);
            NewRelease data = new NewRelease("test");
            Release releaseResult;
            try
            {
                var releases = await client.Repository.Release.GetAll(result.Id);

                releaseResult = await client.Repository.Release.Create(result.Id, data);
            }
            catch (NotFoundException e)
            {
                Console.WriteLine("Not found exception");
            }
            //}

            Console.WriteLine("All done.");
        }
    }
}
