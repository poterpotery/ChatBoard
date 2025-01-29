import React, { useEffect, useState } from 'react';
import {
  StatusBar,
  StyleSheet,
  Text,
  View,
  FlatList,
  TouchableOpacity,
  Image,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import Header from '../../components/Header/Header'; // Assuming you already have this component
import { useNavigation } from '@react-navigation/native';
import ProfileSvg from '../../components/Svgs/ProfileSvg';
import ServerApi from '../../AxiosApi';
import AsyncStorage from '@react-native-async-storage/async-storage';

const Messages = () => {
  const navigation = useNavigation();
  const [users, setAllUsers] = useState([])

  useEffect(() => {
    ServerApi.get("/Auth/GetAllUsers")
      .then(async (response) => {
        let userList = await response?.data?.Data || [];
        setAllUsers(userList);
      })
      .catch((error) => {

      });
  }, []);



  const renderItem = ({ item }) => {
    const randomNumber = Math.floor(1000 + Math.random() * 9000);
    return (
      <TouchableOpacity
        key={item?.userId}
        style={styles.messageItem}
        onPress={() => navigation.navigate('PersonalMessage', { user: item })}
      >

        <ProfileSvg width={40} height={40} />
        <View style={styles.messageContent}>
          <Text style={styles.userName}>{item?.Name == " " ? `User${randomNumber}` : item?.Name}</Text>
          <Text style={styles.userDescription}>Have a good day</Text>
        </View>
      </TouchableOpacity>
    )


  }

  return (
    <View style={styles.container}>
      <StatusBar
        translucent
        backgroundColor="transparent"
        barStyle="dark-content"
      />
      <Header />
      <FlatList
        data={users ? users : []}
        keyExtractor={(item) => item?.UserId?.toString()}
        renderItem={renderItem}
        contentContainerStyle={styles.listContainer}
      />
    </View>
  );
};

export default Messages;

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  listContainer: {
    padding: 10,
  },
  messageItem: {
    flexDirection: 'row',
    alignItems: 'center',
    padding: 10,
    // borderBottomWidth: 1,
    // borderBottomColor: '#ccc',
  },
  userImage: {
    width: 50,
    height: 50,
    borderRadius: 25,
    marginRight: 10,
  },
  messageContent: {
    flex: 1,
  },
  userName: {
    fontSize: 16,
    fontWeight: 'bold',
  },
  userDescription: {
    fontSize: 14,
    color: '#666',
  },
});
