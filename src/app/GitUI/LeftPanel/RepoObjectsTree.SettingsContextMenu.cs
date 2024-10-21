using GitCommands;

namespace GitUI.LeftPanel
{
    partial class RepoObjectsTree
    {
        /// <summary>
        /// We assume tree to position indices are 0-based and sequential. In case this
        /// is no longer true, because for e.g. user has reverted to an earlier version,
        /// this function will fix the indices, attempting to maintain the existing order.
        /// </summary>
        private void FixInvalidTreeToPositionIndices()
        {
            // Sort by index, then force assign 0-based sequential indices
            Dictionary<Tree, int> treeToIndex = GetTreeToPositionIndex();

            int i = 0;
            foreach (KeyValuePair<Tree, int> kvp in treeToIndex.OrderBy(kvp => kvp.Value))
            {
                treeToIndex[kvp.Key] = i;
                ++i;
            }

            SaveTreeToPositionIndex(treeToIndex);
        }

        private Dictionary<Tree, int> GetTreeToPositionIndex()
        {
            return new Dictionary<Tree, int>
            {
                [_favoritesTree] = AppSettings.RepoObjectsTreeFavoritesIndex,
                [_branchesTree] = AppSettings.RepoObjectsTreeBranchesIndex,
                [_remotesTree] = AppSettings.RepoObjectsTreeRemotesIndex,
                [_tagTree] = AppSettings.RepoObjectsTreeTagsIndex,
                [_submoduleTree] = AppSettings.RepoObjectsTreeSubmodulesIndex,
                [_stashTree] = AppSettings.RepoObjectsTreeStashesIndex
            };
        }

        private void SaveTreeToPositionIndex(Dictionary<Tree, int> treeToPositionIndex)
        {
            AppSettings.RepoObjectsTreeFavoritesIndex = treeToPositionIndex[_favoritesTree];
            AppSettings.RepoObjectsTreeBranchesIndex = treeToPositionIndex[_branchesTree];
            AppSettings.RepoObjectsTreeRemotesIndex = treeToPositionIndex[_remotesTree];
            AppSettings.RepoObjectsTreeTagsIndex = treeToPositionIndex[_tagTree];
            AppSettings.RepoObjectsTreeSubmodulesIndex = treeToPositionIndex[_submoduleTree];
            AppSettings.RepoObjectsTreeStashesIndex = treeToPositionIndex[_stashTree];
        }

        private void ReorderTreeNode(TreeNode node, bool up)
        {
            Tree tree = (Tree)node.Tag;
            Dictionary<Tree, int> treeToIndex = GetTreeToPositionIndex();
            Dictionary<int, Tree> indexToTree = treeToIndex.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);

            int currIndex = treeToIndex[tree];

            // Find next visible tree to swap with, if any
            int swapIndex = currIndex;
            do
            {
                swapIndex = up ? swapIndex - 1 : swapIndex + 1;

                // If there are no visible nodes to swap with, we're done
                if (swapIndex < 0 || swapIndex >= treeToIndex.Count)
                {
                    return;
                }
            }
            while (!indexToTree[swapIndex].TreeViewNode.IsVisible);

            Tree swapWithTree = indexToTree[swapIndex];

            // Swap indices
            treeToIndex[tree] = treeToIndex[swapWithTree];
            treeToIndex[swapWithTree] = currIndex;

            // Save new indices
            SaveTreeToPositionIndex(treeToIndex);

            // Remove all trees, then show enabled ones at new indices
            RemoveTree(_branchesTree);
            RemoveTree(_remotesTree);
            RemoveTree(_tagTree);
            RemoveTree(_favoritesTree);
            RemoveTree(_submoduleTree);
            RemoveTree(_stashTree);
            ShowEnabledTrees();
        }

        public void ClearTrees()
        {
            _favoritesTree.ClearTree();
            _branchesTree.ClearTree();
            _remotesTree.ClearTree();
            _tagTree.ClearTree();
            _submoduleTree.ClearTree();
            _stashTree.ClearTree();
        }

        private void ShowEnabledTrees()
        {
            if (tsbShowFavorites.Checked)
            {
                AddTree(_favoritesTree);
            }

            if (tsbShowBranches.Checked)
            {
                AddTree(_branchesTree);
            }

            if (tsbShowRemotes.Checked)
            {
                AddTree(_remotesTree);
            }

            if (tsbShowTags.Checked)
            {
                AddTree(_tagTree);
            }

            if (tsbShowSubmodules.Checked)
            {
                AddTree(_submoduleTree);
            }

            if (tsbShowStashes.Checked)
            {
                AddTree(_stashTree);
            }
        }

        private void tsbShowBranches_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowBranches = tsbShowBranches.Checked;
            _searchResult = null;
            if (tsbShowBranches.Checked)
            {
                AddTree(_branchesTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_branchesTree);
            }
        }

        private void tsbShowRemotes_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowRemotes = tsbShowRemotes.Checked;
            _searchResult = null;
            if (tsbShowRemotes.Checked)
            {
                AddTree(_remotesTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_remotesTree);
            }
        }

        private void tsbShowFavorites_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowFavorites = tsbShowFavorites.Checked;
            _searchResult = null;

            if (tsbShowFavorites.Checked)
            {
                AddTree(_favoritesTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_favoritesTree);
            }
        }

        private void tsbShowTags_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowTags = tsbShowTags.Checked;
            _searchResult = null;
            if (tsbShowTags.Checked)
            {
                AddTree(_tagTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_tagTree);
            }
        }

        private void tsbShowSubmodules_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowSubmodules = tsbShowSubmodules.Checked;
            _searchResult = null;
            if (tsbShowSubmodules.Checked)
            {
                AddTree(_submoduleTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_submoduleTree);
            }
        }

        private void tsbShowStashes_Click(object sender, EventArgs e)
        {
            AppSettings.RepoObjectsTreeShowStashes = tsbShowStashes.Checked;
            _searchResult = null;
            if (tsbShowStashes.Checked)
            {
                AddTree(_stashTree);
                _searchResult = null;
            }
            else
            {
                RemoveTree(_stashTree);
            }
        }
    }
}
