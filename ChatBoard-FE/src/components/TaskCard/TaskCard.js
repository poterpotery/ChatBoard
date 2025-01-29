import { StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import React from 'react';
import EditSvg from '../Svgs/EditSvg';
import DeleteTaskSvg from '../Svgs/DeleteTaskSvg';
import { CheckBox } from 'react-native-elements';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useToast } from 'react-native-toast-notifications';
import { useNavigation } from '@react-navigation/native';

const TaskCard = ({ task, fetchTasks, toggleComplete,userId }) => {
  const toast = useToast()
  const navigation = useNavigation()
  const handleEdit = () => {
    // Navigate to EditTaskBar and pass the task data
    navigation.navigate('EditTaskBar', { taskId: task?.id, taskText: task?.text ,userId});
  };

  const handleDelete = async () => {
    try {
      if (!userId) return; // Ensure userId is available
      const storedTasks = await AsyncStorage.getItem(`tasks_${userId}`);
      const tasks = storedTasks ? JSON.parse(storedTasks) : [];
  
      // Filter out the task to delete
      const updatedTasks = tasks?.filter((t) => t?.id !== task?.id);
  
      // Update AsyncStorage with the updated task list
      await AsyncStorage.setItem(`tasks_${userId}`, JSON.stringify(updatedTasks));
  
      // Display success toast and refresh tasks
      toast.show('Task deleted successfully', { type: 'success' });
      fetchTasks(); // Call fetchTasks to update the UI
    } catch (error) {
      console.error('Error deleting task:', error);
      toast.show('Error deleting task', { type: 'danger' });
    }
  };
  
  
  return (
    <View style={styles.cardContainer}>
      <View style={styles.rightContainer}>
        <CheckBox
          title=""
          checked={task?.isCompleted}
          onPress={() => toggleComplete(task?.id)}
          containerStyle={{ padding: 0 }}
          checkedColor="#2764E7"
          uncheckedColor="grey"
        />
        <Text style={[styles.taskText, task?.isCompleted && styles.completedText]}>
          {task?.text}
        </Text>
      </View>
      <View style={styles.leftContainer}>
        <TouchableOpacity onPress={handleEdit}>
          <EditSvg />
        </TouchableOpacity>
        <TouchableOpacity onPress={handleDelete}>
          <DeleteTaskSvg />
        </TouchableOpacity>
      </View>
    </View>
  );
};

export default TaskCard;

const styles = StyleSheet.create({
  cardContainer: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    backgroundColor: '#F0F4F8',
    borderWidth: 1,
    borderColor: '#0000001A',
    paddingVertical: 15,
    marginVertical: 10,
    borderRadius: 10,
  },
  rightContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '70%',
    paddingLeft: 10,
  },
  leftContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '20%',
    marginRight: 20,
    justifyContent: 'space-between',
  },
  taskText: {
    marginLeft: 10,
    color: '#000',
  },
  completedText: {
    textDecorationLine: 'line-through',
    color: 'grey',
  },
});
