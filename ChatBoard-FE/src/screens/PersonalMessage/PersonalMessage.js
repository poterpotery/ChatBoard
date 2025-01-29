import React, { useEffect, useRef, useState } from 'react';
import {
  StyleSheet,
  Text,
  View,
  FlatList,
  TextInput,
  TouchableOpacity,
  StatusBar,
  BackHandler,
} from 'react-native';
import { SafeAreaView } from 'react-native-safe-area-context';
import Header from '../../components/Header/Header';
import SendSvg from '../../components/Svgs/SendSvg';
import ServerApi from '../../AxiosApi';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/native';
import { useFocusEffect } from '@react-navigation/native';
const PersonalMessage = ({ route }) => {
  const { user } = route.params;
  const [myId, setMyId] = useState("")
  const navigation = useNavigation()
  // State for messages
  const [intervalId, setIntervalId] = useState(null);
  const [messages, setMessages] = useState([]);
  const flatListRef = useRef();
  // State for new message input
  const [newMessage, setNewMessage] = useState('');

  // Function to send a message
  const handleSendMessage = () => {
    if (newMessage?.trim() === '') return;

    const newMessageObject = {
      MessageId: messages.length + 1, // Unique ID
      Content: newMessage?.trim(),
      SenderId: myId,
      ReceiverId: user?.UserId,
      SentAt: new Date().toISOString(), // Current timestamp
      IsMine: true,
    };

    // Locally update messages
    setMessages((prevMessages) => [newMessageObject, ...prevMessages]);
    setNewMessage(''); // Clear the input field
    let body = {
      "receiverId": user?.UserId,
      "content": newMessage?.trim()
    }
    ServerApi.post('/Auth/SendMessage', body).then(async (response) => {
   
    }).catch((error) => {

    })
  };
  const renderItem = ({ item }) => (
    <View
      key={item?.SenderId}
      style={[
        styles.messageBubble,
        item?.SenderId == myId
          ?
          styles.senderBubble : styles.receiverBubble,
      ]}
    >
      <Text style={styles.messageText}>{item?.Content}</Text>
    </View>
  );



  useFocusEffect(
    React.useCallback(() => {
      const fetchMessages = async () => {
        try {
          const response = await ServerApi.get(`/Auth/GetMessages?OtherUserId=${user?.UserId}&PageNumber=1&PageSize=50`);
          const messages = response?.data?.Data || [];
          setMessages(messages);
        } catch (error) {
          console.error("Error fetching messages: ", error);
        }
      };

      // Start fetching messages immediately on screen focus
      fetchMessages();

      // Set up the interval for periodic fetching
      const id = setInterval(fetchMessages, 10000); // Fetch every 10 seconds
      setIntervalId(id);

      // Cleanup interval on screen blur
      return () => {
        clearInterval(id);
      };
    }, [])
  );


  useEffect(() => {
    const fetchId = async () => {
      try {
        const id = await AsyncStorage.getItem('Id');
        setMyId(id);
      } catch (error) {
        console.error('Error fetching ID:', error);
      }
    };

    fetchId();
  }, []);

  // const handleBackButton = () => {
  //   // navigation.push("TaskbarStacks");
  //   navigation.navigate('TaskBar')
  //   return true; // Prevent default behavior (exit the app)
  // };
  // useEffect(() => {
  //   // Add event listener for the Android back button
  //   BackHandler.addEventListener('hardwareBackPress', handleBackButton);

  //   // Clean up the event listener when the component is unmounted
  //   return () => {
  //     BackHandler.removeEventListener('hardwareBackPress', handleBackButton);
  //   };
  // }, []);

  useEffect(() => {
    // Scroll to bottom whenever messages change
    flatListRef.current?.scrollToEnd({ animated: true });
  }, [messages]);
  return (
    <>
      <StatusBar
        translucent
        backgroundColor="transparent"
        barStyle="dark-content"
      />
      <Header />
      <View style={styles.container}>
        <FlatList
        ref={flatListRef}
          data={messages ? [...messages]?.reverse() : []} // Reverse the array
          keyExtractor={(item) => item?.MessageId?.toString()} // Ensure the key is a string
          renderItem={renderItem}
          contentContainerStyle={styles.chatContainer}
        />
        <View style={styles.inputContainer}>
          <TextInput
            style={styles.input}
            placeholder="Type a message..."
            value={newMessage}
            onChangeText={setNewMessage}
          />
          <TouchableOpacity onPress={handleSendMessage} style={styles.sendButton}>
            <SendSvg />
          </TouchableOpacity>
        </View>
      </View>
    </>

  );
};

export default PersonalMessage;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 10,
  },
  chatContainer: {
    padding: 10,
    flexGrow: 1,
    justifyContent: 'flex-end',
  },
  messageBubble: {
    maxWidth: '80%',
    padding: 10,
    marginVertical: 5,
    borderRadius: 10,
  },
  senderBubble: {
    alignSelf: 'flex-end',
    backgroundColor: '#EDA0A8',
  },
  receiverBubble: {
    alignSelf: 'flex-start',
    backgroundColor: '#CCF3CF',
  },
  messageText: {
    fontSize: 16,
  },
  inputContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    paddingHorizontal: 10,
    paddingVertical: 5,
  },
  input: {
    flex: 1,
    height: 40,
    borderWidth: 1,
    borderColor: '#ccc',
    paddingHorizontal: 10,
    backgroundColor: "#F0F4F8",
    borderTopLeftRadius: 10,
    borderBottomLeftRadius: 10
  },
  sendButton: {
    width: 50,
    backgroundColor: '#504B4B',
    height: 40,
    justifyContent: "center",
    alignItems: "center",
    borderTopRightRadius: 10,
    borderBottomRightRadius: 10
  },
});
