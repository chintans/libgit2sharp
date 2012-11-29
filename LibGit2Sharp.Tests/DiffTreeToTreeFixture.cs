﻿using System;
using System.IO;

        [Fact]
        public void CanCompareATreeAgainstANullTree()
        {
            using (var repo = new Repository(StandardTestRepoPath))
            {
                Tree tree = repo.Branches["refs/remotes/origin/test"].Tip.Tree;

                TreeChanges changes = repo.Diff.Compare(tree, null);

                Assert.Equal(1, changes.Count());
                Assert.Equal(1, changes.Deleted.Count());

                Assert.Equal("readme.txt", changes.Deleted.Single().Path);

                changes = repo.Diff.Compare(null, tree);

                Assert.Equal(1, changes.Count());
                Assert.Equal(1, changes.Added.Count());

                Assert.Equal("readme.txt", changes.Added.Single().Path);
            }
        }

        [Fact]
        public void ComparingTwoNullTreesReturnsAnEmptyTreeChanges()
        {
            using (var repo = new Repository(StandardTestRepoPath))
            {
                TreeChanges changes = repo.Diff.Compare(null, null, null);

                Assert.Equal(0, changes.Count());
            }
        }