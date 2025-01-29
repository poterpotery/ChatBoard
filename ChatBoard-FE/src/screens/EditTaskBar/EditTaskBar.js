import React, { useState } from 'react';
import { StyleSheet, Text, TextInput, TouchableOpacity, View } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import Header from '../../components/Header/Header';
import { useToast } from 'react-native-toast-notifications';

const EditTaskBar = ({ navigation, route }) => {
  const { taskId, taskText ,userId} = route.params; // Receiving task data via navigation parameters
  const [newText, setNewText] = useState(taskText);
  const toast = useToast()
  const handleUpdateTask = async () => {
    try {
      if (!userId) return; // Ensure userId is available
  
      // Retrieve the existing tasks for the specific user
      const storedTasks = await AsyncStorage.getItem(`tasks_${userId}`);
      const tasks = storedTasks ? JSON.parse(storedTasks) : [];
  
      // Update the specific task
      const updatedTasks = tasks.map((task) =>
        task?.id === taskId ? { ...task, text: newText } : task
      );
  
      // Save the updated tasks back to AsyncStorage
      await AsyncStorage.setItem(`tasks_${userId}`, JSON.stringify(updatedTasks));
  
      // Show success toast
      toast.show('Task updated successfully', {
        type: 'success',
        placement: 'top',
        offset: 30,
      });
  
      // Navigate back to the previous screen
      navigation.goBack();
    } catch (error) {
      console.error('Error updating task:', error);
      toast.show('Error updating task', { type: 'danger' });
    }
  };
  

  const handleCancel = () => {
    // Navigate back to the previous screen without saving changes
    navigation.goBack();
  };

  return (
    <>
      <Header />
      <View style={styles.container}>
        <TextInput
          style={styles.input}
          value={newText}
          onChangeText={setNewText}
          placeholder="Edit your task"
        />
        <View style={styles.buttonContainer}>
          <TouchableOpacity style={styles.updateButton} onPress={handleUpdateTask}>
            <Text style={styles.buttonText}>Save Changes</Text>
          </TouchableOpacity>
          <TouchableOpacity style={styles.cancelButton} onPress={handleCancel}>
            <Text style={styles.buttonText}>Cancel</Text>
          </TouchableOpacity>
        </View>
      </View>
    </>
  );
};

export default EditTaskBar;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#F9FAFB',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
    textAlign: 'center',
  },
  input: {
    width: "100%",
    height: 200,
    color: "black",
    fontSize: 13,
    borderRadius: 17,
    paddingLeft: 10,
    backgroundColor: "#F0F4F8",
    borderWidth: 1,
    borderColor: "#0000001A",
    marginVertical: 10,
    textAlignVertical: "top",
    paddingHorizontal: 20,
    fontFamily: "Ubuntu-Regular"
  },
  buttonContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
  },
  updateButton: {
    backgroundColor: '#4CD964',
    padding: 15,
    borderRadius: 100,
    flex: 1,
    marginRight: 10,
    alignItems: 'center',
  },
  cancelButton: {
    backgroundColor: '#D9684C',
    padding: 15,
    borderRadius: 100,
    flex: 1,
    marginLeft: 10,
    alignItems: 'center',
  },
  buttonText: {
    color: 'black',
    fontSize: 16,
    fontFamily: "Ubuntu-Bold"
  },
});
