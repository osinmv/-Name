using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person
{
	private bool gender;
	private List<SkillStructure> skills = new List<SkillStructure>();


	public Person(List<SkillStructure> skills)
	{
		if (Random.Range (0, 1) == 0) {
			this.gender = true;
		} else {
			this.gender = false;
		}	
		this.skills = skills;
	}
	public bool Gender
	{
		get{ return gender;}

	}
	public List<SkillStructure> Skills
	{
		get{ return skills;}

	}
}
public class SkillStructure {

	private string Skillname;
	private int profecionalism;

	public SkillStructure(string name)
	{
		this.Skillname = name;
		this.profecionalism = Random.Range (0, 10);

	}
	public int Profecionalism
	{
		get{ return profecionalism;}

	}
}
