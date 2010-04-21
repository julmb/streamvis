// Copyright © Julian Brunner 2009 - 2010

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

#ifndef __STREAMVIS_WRAPPERS_ROS_H__
#define __STREAMVIS_WRAPPERS_ROS_H__

#include <ros/ros.h>
#include <topic_tools/shape_shifter.h>

extern "C"
{
	ros::NodeHandle* Initialize();
	void Dispose(ros::NodeHandle* nodeHandle);
	ros::Publisher* Advertise(ros::NodeHandle* nodeHandle, const char* topicName, unsigned int queueLength);
	void DisposePublisher(ros::Publisher* publisher);
	ros::Subscriber* Subscribe(ros::NodeHandle* nodeHandle, const char* topicName, unsigned int queueLength, void (*callback)(topic_tools::ShapeShifter::ConstPtr));
	void DisposeSubscriber(ros::Subscriber* subscriber);
	bool ros_ok();
	void ros_spinOnce();
}

#endif
