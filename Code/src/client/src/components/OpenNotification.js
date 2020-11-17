import { notification } from 'antd';
export const openNotification = (type, message, description) => {
  notification.config({
    duration: 6,
    placement: 'topRight',
  });
  notification[type]({
    message: message,
    description: description,
  });
};
