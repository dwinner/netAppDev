package com.packt.poiapp;

import javax.xml.bind.annotation.XmlRootElement;

@XmlRootElement(name = "pois")
public class PointOfInterest {
	
	private int id;
	private String name;
	private String description;
	private double latitude;
	private double longitude;
	private String image;
	private String address;

	public int getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public double getLatitude() {
		return latitude;
	}

	public void setLatitude(double latitude) {
		this.latitude = latitude;
	}

	public double getLongitude() {
		return longitude;
	}

	public void setLongitude(double longitude) {
		this.longitude = longitude;
	}

	public String getImage() {
		return image;
	}

	public void setId(int id) {
		this.id = id;
	}

	public void setImage(String image) {
		this.image = image;
	}

	public String getAddress() {
		return address;
	}

	public void setAddress(String address) {
		this.address = address;
	}

	@Override
	public String toString() {
		return "{id:" + id + ", name" + name + ", description:" + description 
				+ ", lat" + latitude + " , lon:" + longitude + ", image:"
				+ image + ", address:" + address + "}";
	}
}
