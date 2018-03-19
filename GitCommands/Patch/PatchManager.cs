        private List<Patch> _patches = new List<Patch>();

        public List<Patch> Patches
        {
            get { return _patches; }
            set { _patches = value; }
        }

            string header;

            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, staged, out header);
            //git apply has problem with dealing with autocrlf
            //I noticed that patch applies when '\r' chars are removed from patch if autocrlf is set to true

            string header;

            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, staged, out header);
            //if file is new, --- /dev/null has to be replaced by --- a/fileName
            else
                return GetPatchBytes(header, body, fileContentEncoding);
            string[] headerLines = header.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            string fileMode = "100000";//given fake mode to satisfy patch format, git will override this
            //git apply has problem with dealing with autocrlf
            //I noticed that patch applies when '\r' chars are removed from patch if autocrlf is set to true
        //TODO encoding for each file in patch should be obtained separately from .gitattributes
            _patches = patchProcessor.CreatePatchesFromString(text).ToList();
            foreach (Patch patchApply in _patches)



            //stage no new line at the end only if last +- line is selected
        //patch base is changed file
        private int StartLine;
        private List<SubChunk> SubChunks = new List<SubChunk>();
                    SubChunks.Add(_CurrentSubChunk);
            //if postContext is not empty @line comes from next SubChunk
                _CurrentSubChunk = null;//start new SubChunk
            return int.TryParse(header, out StartLine);
                    //do not refactor, there are no break points condition in VS Experss

            result.StartLine = 0;
                //do not refactor, there are no breakpoints condition in VS Experss
                            //if the last line is selected to be reset and there is no new line at the end of file
                            //then we also have to remove the last not selected line in order to add it right again with the "No newline.." indicator

            foreach (SubChunk subChunk in SubChunks)
            diff = "@@ -" + StartLine + "," + removedCount + " +" + StartLine + "," + addedCount + " @@".Combine("\n", diff);

            //When there is no patch, return nothing
                //if selection intersects with chunsk
            SubChunkToPatchFnc subChunkToPatch = (SubChunk subChunk, ref int addedCount, ref int removedCount, ref bool wereSelectedLines) =>
                {
                    return subChunk.ToResetUnstagedLinesPatch(ref addedCount, ref removedCount, ref wereSelectedLines);
                };
            return ToPatch(subChunkToPatch);
            SubChunkToPatchFnc subChunkToPatch = (SubChunk subChunk, ref int addedCount, ref int removedCount, ref bool wereSelectedLines) =>
            };
            return ToPatch(subChunkToPatch);




