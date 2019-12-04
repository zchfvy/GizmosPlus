
build:
	doxygen Doxyfile

clean:
	rm -rf html
	rm -f html.meta

rebuild: clean build

update-gh-pages:
	git checkout gh-pages
	git merge master --no-edit
	doxygen Doxyfile
	git add html
	git commit -m "Update documentation"
	git checkout master

push-gh-pages:
	git checkout gh-pages
	git push
	git checkout master

all: rebuild update-gh-pages push-gh-pages
