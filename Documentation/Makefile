
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

