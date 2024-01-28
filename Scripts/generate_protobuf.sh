#!/bin/sh
SOURCE_DIR=../Schema/protobuf
SOURCE_FILES=../Schema/protobuf/*.proto
CSHARP_DIR=../LtAmpDotNet/LtAmpDotNet.Lib/Model/Protobuf
DOC_DIR=../Docs
DOC_FILE=protobuf.md

case $1 in
  "csharp")
    protoc -I=$SOURCE_DIR --csharp_out=$CSHARP_DIR $SOURCE_FILES
    ;;
  #"doc")
  #  protoc -I=$SOURCE_DIR --doc_opt=markdown,$DOC_FILE --doc_out=$DOC_DIR $SOURCE_FILES
  #  ;;
esac