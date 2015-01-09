@echo off

set libs=-luser32 -ladvapi32 -lkernel32
set out=Sling.Launcher.exe

mkdir bin
mkdir obj

cls
echo ==========================================
echo Sling.Launcher Compiler
echo ==========================================
echo.

echo Cleaning objects
del /Q obj\main.o
del /Q obj\win_check.o

echo Compiling main.c
tcc -c -o obj/main.o main.c
echo Compiling win_check.c
tcc -c -o obj/win_check.o win_check.c

tcc -o bin/%out% obj/main.o obj/win_check.o %libs% || goto link_fail
echo Linking successful!
bin\%out%

:link_fail