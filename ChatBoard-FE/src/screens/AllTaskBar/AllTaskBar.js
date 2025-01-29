import React, { useEffect, useState } from 'react';
import { StatusBar, StyleSheet, Text, TextInput, TouchableOpacity, View, FlatList } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { SafeAreaView } from 'react-native-safe-area-context';
import Header from '../../components/Header/Header';
import TaskCard from '../../components/TaskCard/TaskCard';
import { useIsFocused } from '@react-navigation/native';
import { useToast } from 'react-native-toast-notifications';

const AllTaskBar = () => {
  const [tasks, setTasks] = useState([]);
  const [taskText, setTaskText] = useState('');
  const isFocused = useIsFocused();
  const toast = useToast();
  const [userId, setUserId] = useState(null); // Store the user ID in state

  useEffect(() => {
    const fetchId = async () => {
      const id = await AsyncStorage.getItem('Id');
      setUserId(id); // Set the user ID
    };

    fetchId();
  }, []);

  useEffect(() => {
    if (userId) {
      fetchTasks();
    }
  }, [isFocused, userId]);

  const fetchTasks = async () => {
    try {
      if (!userId) return;
      const storedTasks = await AsyncStorage.getItem(`tasks_${userId}`);
      setTasks(storedTasks ? JSON.parse(storedTasks) : []);
    } catch (error) {
      console.error('Error fetching tasks:', error);
    }
  };

  const handleAddTask = async () => {
    if (taskText.trim() === '') return;

    const newTask = {
      id: Date.now(),
      text: taskText,
      isCompleted: false,
    };

    try {
      const updatedTasks = [...tasks, newTask];
      await AsyncStorage.setItem(`tasks_${userId}`, JSON.stringify(updatedTasks));
      setTaskText('');
      toast.show('Task Added Successfully', {
        type: 'success',
        placement: 'top',
        offset: 300,
      });
      fetchTasks();
    } catch (error) {
      console.error('Error adding task:', error);
      toast.show('Something Wrong!', {
        type: 'danger',
        placement: 'top',
        offset: 300,
      });
    }
  };

  const handleToggleComplete = async (taskId) => {
    try {
      const updatedTasks = tasks.map((task) =>
        task?.id === taskId ? { ...task, isCompleted: !task?.isCompleted } : task
      );
      await AsyncStorage.setItem(`tasks_${userId}`, JSON.stringify(updatedTasks));
      fetchTasks();
    } catch (error) {
      console.error('Error toggling task completion:', error);
    }
  };

  return (
    <View>
      <StatusBar
        translucent
        backgroundColor="transparent"
        barStyle="dark-content"
      />
      <Header />
      <View style={styles.inputContainer}>
        <TextInput
          style={styles.input}
          value={taskText}
          onChangeText={setTaskText}
          placeholder="Write task"
          placeholderTextColor="grey"
        />
        <TouchableOpacity style={styles.btnAddTask} onPress={handleAddTask}>
          <Text style={styles.btnText}>+ Add task</Text>
        </TouchableOpacity>
      </View>
      <FlatList
        data={tasks}
        keyExtractor={(item) => item?.id?.toString()}
        renderItem={({ item }) => (
          <TaskCard
            task={item}
            fetchTasks={fetchTasks}
            toggleComplete={handleToggleComplete}
            userId={userId}
          />
        )}
        contentContainerStyle={{ padding: 10 }}
      />
    </View>
  );
};

export default AllTaskBar;


const styles = StyleSheet.create({
  inputContainer: {
    width: "100%",
    justifyContent: "center",
    alignItems: "center",
  },
  input: {
    width: "90%",
    height: 100,
    color: "black",
    fontSize: 13,
    borderRadius: 17,
    paddingLeft: 10,
    backgroundColor: "#F0F4F8",
    borderWidth: 1,
    borderColor: "#0000001A",
    marginVertical: 10,
    textAlignVertical: "top",
  },
  btnAddTask: {
    paddingVertical: 10,
    paddingHorizontal: 15,
    borderRadius: 10,
    backgroundColor: "#504B4B",
    justifyContent: "center",
    alignSelf: "flex-end",
    marginRight: 25,
  },
  btnText: {
    alignSelf: "flex-start",
    color: "white",
  },
});
