#include"myDll.h"
#include "pch.h"


int __stdcall Fact(int n)
{
	int res = 1;
	for (int i = 1; i <= n; i++)
		res *= i;
	return res;
}

double __stdcall Sub(double a, double b)
{
	/*
	double res = a - b;
	if (res < 0)
		return -res;
	else
		return res;
	*/
	return a - b;
}
double __stdcall Add(double a, double b)
{
	return a + b;
}

double __stdcall Multi(double a, double b)
{
	return a * b;
}

double __stdcall Diverse(double a, double b)
{
	return a / b;
}
