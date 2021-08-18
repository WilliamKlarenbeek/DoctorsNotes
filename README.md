# DoctorsNotes
### The TL;DR
If you already know how to use Git, here's what you need to know about working with the Doctors Notes repository.

1. **Use development branches!** Never commit anything to master. Create your own development branch any time you want to start working on a new feature or fix. Make sure you give it a name that makes sense, such as `Dev-VoxelDeformation` if you want to be specific about the feature you're working on. Don't be afraid to create lots of branches, just make sure you can keep track of them.
1. **Write good commit messages!** Commit messages should be imperative, not descriptive, ie. the commit message should say what the commit *will do* to the repo once it's applied, not what the change *has done* to the repo. For example:
	1. 'Added new water effects' ❌
	1. 'Add new water effects' ✅
	1. 'Fixed bugs' ❌
	1. 'Fix bug relating to water shader'✅
1. **Use pull requests!** When you push a change that is ready to go into master, create a pull request and get someone else to check it over before merging it. Checking over a pull request means testing it and (if it includes code changes) reviewing the code.

### Getting Git on your PC
Using your favourite web browser, navigate to [https://git-scm.com/downloads](https://git-scm.com/downloads). Choose your OS, download and run the installer.

If you are using Windows, I can recommend ticking the Windows Explorer integration options. These will allow you to open the Git GUI and command line by right clicking in a directory. This can save time navigating from your home folder to the folder where you store your projects. Mac OS may have a similar feature, though I don't have a Mac system to test this on.

Other than the above, all other options are fine to leave as default, *unless you want to edit your commit messages with an editor aside from vim*, in which case choose your favourite text editor when the option is made available.

If you want to, you are welcome to use a Git GUI, however this guide assumes the use of Git bash and the command line, which can be more efficient and easier to use in the long run.

### The Basics
#### Setting up the repo
If you haven't set up the Doctors Notes repo yet, or you just want a fresh start, you can run `git clone https://github.com/williamklarenbeek/doctorsnotes` and the repo will be downloaded into a folder called 'DoctorsNotes' in the directory that the command was run.

#### Getting the latest changes
While Git bash is open in the 'DoctorsNotes' directory, run `git pull` to get the latest changes. This will pull all changes from the remote repository into your local repository. This will include any changes that are not in your current branch, ie. this command will update *all* branches in your local repo with any changes in the remote repo.

*Note:* The 'local repository' and the 'remote repository' differ in only one way - where they are physically stored. The 'remote repository' is stored on GitHub's servers and can be accessed by anyone in the team. The 'local repository' is local to your machine, and is accessible only by you.

#### Branches
Branches are a way of logically seperating different pieces of work in Git. Branches are essentially named copies of the codebase that you can swap between without losing your changes. Every repository comes fully kitted with exactly one branch, called `master`. This is the 'master copy' of the repository.

To create a new branch run `git checkout -b new_branch`. This will create a copy of the current branch in a new branch called `new_branch` (provided one does not already exist) and move you to that branch.

To move to another branch run `git checkout branch_name`.

To list all branches in the repo, run `git branch`.

To delete a branch, run `git branch -D`. This will ***permanently*** delete that branch and any code on it, so make sure you really, *really*, ***really*** wanna do this!

#### Gitting stuff done (and committing your changes)
You've just done a bunch of work in local repo and you want to upload it to the remote repo so that the rest of the team can test it, improve it or otherwise access it. How do you do this?

1. Run `git status` to see which files you've added, deleted or changed. Make sure this is what you expect! It's very easy to accidentally edit other files without realising.
1. Run `git add --all` to stage *all* changes.
	1. If you only want to add one, or a few files, you can explicitly define those with `git add <file_name>` or `git add <directory_name>`.
	1. If you want to remove any files from your staging area, run `git reset HEAD <file_name>` or `git reset HEAD <directory_name>`
1. Commit your changes by running `git commit`. This will open a text file for you to supply a commit message. This can be done inline in the command line by running `git commit -m <commit_message>`.
1. Run `git push origin <branch_name>` to push your changes to the remote repository.
1. To merge your code into `master`, create a pull request (see below).

#### Pull requests
Pull requests are the way GitHub handles merging branches together. While this can be accomplished using only Git commands, pull requests make it easier to get changes reviewed by someone and merged in.

Once your change is pushed to the remote repository on your development branch, create a pull request via GitHub. You can do this by doing the following:

1. Go to the Doctors Notes repository on GitHub and navigate to 'Pull Requests'.
1. Click 'New Pull Request'.
1. There should be two dropdowns at the top of the page. Set the 'base' to `master` and the 'compare' to your dev branch. This will display a screen with all the differences between your the two branches.
1. Click 'Create Pull Request' to create the request and make it visible to the team.
1. Get someone else to review your changes and merge the request. Hooray! Your changes are now in master!

#### Adding new assets to git
If you've just created a new asset and you want to either test it or get it onto the git repo, follow the below steps.

1. Open git bash and pull the latest change from master.
1. Open the Marching Cubes project in Unity.
1. Duplicate the 'MASTERSCENE' scene in the levels folder using Edit > Duplicate. Rename your scene something like TestBuildingAssets (or whatever is relavent to whatever it is you're testing).
1. Test that your assets work. If you want your asssets to be in the new 'MASTERSCENE', feel free to add them to the 'MASTERSCENE' as well.
1. Commit your changes and push them to master.
